﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{

    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
