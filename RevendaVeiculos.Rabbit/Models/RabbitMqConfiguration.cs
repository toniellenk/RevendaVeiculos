﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevendaVeiculos.Rabbit.Models
{
    public class RabbitMqConfiguration
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
