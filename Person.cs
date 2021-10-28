using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Mykyta_Zherdiev
{
    public class Person
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public double Grade { get; set; }

        public string ConvertToString { get => $"{Name}{Year}{Grade}"; }

    }
}
