using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Mykyta_Zherdiev
{
    class PersonComputedValue
    {
        public Person OriginalData { get; set; }

        public string ComputedData { get; set; }

        public PersonComputedValue(Person originalData, string computedData)
        {
            OriginalData = originalData;
            ComputedData = computedData;
        }

        public override string ToString()
        {
            return $"{OriginalData.Name} {OriginalData.Grade} {OriginalData.Year}  {ComputedData}";
        }
    }
}
