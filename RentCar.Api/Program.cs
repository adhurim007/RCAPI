using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RentCar.Application;
using RentCar.Application.Auditing;
using RentCar.Application.Authorization;
using RentCar.Application.Features.Reservations.Validators;
using RentCar.Application.Localization;
using RentCar.Application.MultiTenancy;
using RentCar.Application.Notifications;
using RentCar.Application.Pricing;
using RentCar.Application.Reports;
using RentCar.Application.Services;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using RentCar.Infrastructure;
using RentCar.Persistence;
using RentCar.Persistence.Identity;
using RentCar.Persistence.Repositories;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ----------------- Logging -----------------
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ----------------- Services -----------------

builder.Services.AddDbContext<RentCarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

// ✅ Use Identity with GUID everywhere
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<RentCarDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<IPricingEngine, PricingEngine>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ILocalizationService, CachedLocalizationService>();

builder.Services.AddAuthorization(options =>
{
    // Cars
    options.AddPolicy(Permissions.Cars.View, p => p.RequireClaim("Permission", Permissions.Cars.View));
    options.AddPolicy(Permissions.Cars.Create, p => p.RequireClaim("Permission", Permissions.Cars.Create));
    options.AddPolicy(Permissions.Cars.Update, p => p.RequireClaim("Permission", Permissions.Cars.Update));
    options.AddPolicy(Permissions.Cars.Delete, p => p.RequireClaim("Permission", Permissions.Cars.Delete));
    options.AddPolicy(Permissions.Cars.ManageImages, p => p.RequireClaim("Permission", Permissions.Cars.ManageImages));

    // Car Brands
    options.AddPolicy(Permissions.CarBrands.View, p => p.RequireClaim("Permission", Permissions.CarBrands.View));
    options.AddPolicy(Permissions.CarBrands.Create, p => p.RequireClaim("Permission", Permissions.CarBrands.Create));
    options.AddPolicy(Permissions.CarBrands.Update, p => p.RequireClaim("Permission", Permissions.CarBrands.Update));
    options.AddPolicy(Permissions.CarBrands.Delete, p => p.RequireClaim("Permission", Permissions.CarBrands.Delete));

    // Car Models
    options.AddPolicy(Permissions.CarModels.View, p => p.RequireClaim("Permission", Permissions.CarModels.View));
    options.AddPolicy(Permissions.CarModels.Create, p => p.RequireClaim("Permission", Permissions.CarModels.Create));
    options.AddPolicy(Permissions.CarModels.Update, p => p.RequireClaim("Permission", Permissions.CarModels.Update));
    options.AddPolicy(Permissions.CarModels.Delete, p => p.RequireClaim("Permission", Permissions.CarModels.Delete));

    // Car Pricing Rules
    options.AddPolicy(Permissions.CarPricingRules.View, p => p.RequireClaim("Permission", Permissions.CarPricingRules.View));
    options.AddPolicy(Permissions.CarPricingRules.Create, p => p.RequireClaim("Permission", Permissions.CarPricingRules.Create));
    options.AddPolicy(Permissions.CarPricingRules.Update, p => p.RequireClaim("Permission", Permissions.CarPricingRules.Update));
    options.AddPolicy(Permissions.CarPricingRules.Delete, p => p.RequireClaim("Permission", Permissions.CarPricingRules.Delete));

    // Reservations
    options.AddPolicy(Permissions.Reservations.View, p => p.RequireClaim("Permission", Permissions.Reservations.View));
    options.AddPolicy(Permissions.Reservations.Create, p => p.RequireClaim("Permission", Permissions.Reservations.Create));
    options.AddPolicy(Permissions.Reservations.Update, p => p.RequireClaim("Permission", Permissions.Reservations.Update));
    options.AddPolicy(Permissions.Reservations.Cancel, p => p.RequireClaim("Permission", Permissions.Reservations.Cancel));
    options.AddPolicy(Permissions.Reservations.Approve, p => p.RequireClaim("Permission", Permissions.Reservations.Approve));
    options.AddPolicy(Permissions.Reservations.Reject, p => p.RequireClaim("Permission", Permissions.Reservations.Reject));
    options.AddPolicy(Permissions.Reservations.ViewHistory, p => p.RequireClaim("Permission", Permissions.Reservations.ViewHistory));
    options.AddPolicy(Permissions.Reservations.AddHistory, p => p.RequireClaim("Permission", Permissions.Reservations.AddHistory));
    options.AddPolicy(Permissions.Reservations.GenerateContract, p => p.RequireClaim("Permission", Permissions.Reservations.GenerateContract));

    // Reports
    options.AddPolicy(Permissions.Reports.View, p => p.RequireClaim("Permission", Permissions.Reports.View));

    // Contracts
    options.AddPolicy(Permissions.Contracts.Generate, p => p.RequireClaim("Permission", Permissions.Contracts.Generate));

    // Menus
    options.AddPolicy(Permissions.Menus.Add, p => p.RequireClaim("Permission", Permissions.Menus.Add));
    options.AddPolicy(Permissions.Menus.AssignToRole, p => p.RequireClaim("Permission", Permissions.Menus.AssignToRole));
    options.AddPolicy(Permissions.Menus.ViewByRole, p => p.RequireClaim("Permission", Permissions.Menus.ViewByRole));
    options.AddPolicy(Permissions.Menus.Manage, p => p.RequireClaim("Permission", Permissions.Menus.Manage));

    // Payments
    options.AddPolicy(Permissions.Payments.Add, p => p.RequireClaim("Permission", Permissions.Payments.Add));
    options.AddPolicy(Permissions.Payments.Confirm, p => p.RequireClaim("Permission", Permissions.Payments.Confirm));
});

builder.Services.AddScoped<INotificationService, EmailNotificationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICarPricingRuleRepository, CarPricingRuleRepository>();
builder.Services.AddScoped<IReservationValidator, ReservationValidator>();
builder.Services.AddScoped<IContractPdfGenerator, ContractPdfGenerator>();
builder.Services.AddScoped<ReportGenerator>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

builder.Services.AddAutoMapper(typeof(AssemblyMarker).Assembly);

builder.Services.AddControllers()
    .AddFluentValidation(cfg =>
        cfg.RegisterValidatorsFromAssemblyContaining<AssemblyMarker>());

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "RentCar API", Version = "v1" });

    // 🔒 Safe XML docs inclusion
    try
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        if (File.Exists(xmlPath))
        {
            opt.IncludeXmlComments(xmlPath);
        }
        else
        {
            Console.WriteLine($"⚠️ Swagger XML file not found at {xmlPath}, skipping XML comments.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ Swagger XML include failed: {ex.Message}");
    }

    // ✅ Swagger JWT support
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});



// ✅ JWT Authentication (before app.Build)
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

// ----------------- Build -----------------
var app = builder.Build();

// ✅ Role seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    await RoleSeeder.SeedAsync(roleManager, userManager);
}

// ----------------- Middleware -----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentCar API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
