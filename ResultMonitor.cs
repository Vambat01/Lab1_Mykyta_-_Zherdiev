using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab1_Mykyta_Zherdiev
{
    class ResultMonitor
    {
        private PersonComputedValue[] computedData;
        private object locker = new object();

        public ResultMonitor(int fulllength)
        {
            this.computedData = new PersonComputedValue[fulllength];
        }

        public void AddItemSorted(PersonComputedValue item) 
        {
            lock (locker) 
            {
                for (int i = 0; i < computedData.Length; i++)
                {
                    if (computedData[i] == null) 
                    {
                        computedData[i] = item;                    
                    }
                    else 
                    {
                        if (item.ComputedData[0]<computedData[i].ComputedData[0])
                        {
                            for (int k = computedData.Length - 2; k >= i ; k--)
                            {
                                computedData[k + 1] = computedData[k];
                            }
                            computedData[i] = item;
                            break;

                        }                    
                    }
                }
            
            }
        
        }
        public void WriteAllItemsToFile() 
        {
            using (StreamWriter writetext = new StreamWriter("result.txt"))
            {
                foreach (var item in computedData)
                {
                    writetext.WriteLine(item.ToString());
                }
                //Evrica 
                //Lab1_Mykyta_Zherdiev.PersonComputedValue[]
            }
        }
    }
}
