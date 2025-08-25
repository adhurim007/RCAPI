using Microsoft.AspNetCore.Mvc;

namespace RentCar.Api.Controllers
{
    public class TranslationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
