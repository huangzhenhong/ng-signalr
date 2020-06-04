﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalMonitoring.API.Models
{
    public class SignalViewModel
    {
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string Area { get; set; }
        public string Zone { get; set; }

        public string SignalStamp { get; set; }
    }
}
