﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{    
    public class Tool : Equipment
    {

        public Tool(string name, string type) : base(name, type)
        {
            this.isAvailable = true;
        }
    }
}
