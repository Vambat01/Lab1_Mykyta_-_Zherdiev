using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_Mykyta_Zherdiev
{
    public class DataMonitor
    {

        private Person[] data;
        private object locker = new object();

        public DataMonitor(int dataSize)
        {
            this.data = new Person[dataSize];
        }
        public bool IsNotEmpty()
        {
            return data.Any(x => x != null);

        }

        public void AddItem(Person item)
        {
            while (data.All(x => x != null)) ;


            lock (locker)
            {
                int index = -1;
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] is null)
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                {
                    data[index] = item;
                }
                else
                {
                    //

                }
            }


        }
        public Person RemoveItem()
        {
            lock (locker)
            {

                Person buffer = null;
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    if (data[i] != null)
                    {
                        buffer = data[i];
                        data[i] = null;
                        break;

                    }
                }
                return buffer;

            }
        }

    }
}
