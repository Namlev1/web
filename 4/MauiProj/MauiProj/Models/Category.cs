﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiProj.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return this.name;

        }
    }
}
