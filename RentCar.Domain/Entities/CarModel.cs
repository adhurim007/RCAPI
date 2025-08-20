﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }
    }
}
