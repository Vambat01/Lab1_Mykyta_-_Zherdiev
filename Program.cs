using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Lab1_Mykyta_Zherdiev
{
    class Program
    {
        static object locker = new object();
        static bool additionFinished = false;
        private static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {

                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
        public static void Working(DataMonitor dataArray, ResultMonitor resultArray)
        {

            while (true)
            {
                lock (locker)
                {
                    if (dataArray.IsNotEmpty() || !additionFinished)
                    {

                        Person onLook = dataArray.RemoveItem();
                        string hash = Hash(onLook.ConvertToString);
                        if (char.IsLetter(hash[0]))
                        {
                            PersonComputedValue item = new PersonComputedValue(onLook, hash);
                            resultArray.AddItemSorted(item);
                        }
                    }
                    //else
                    //{
                    //    Thread.Sleep(20);
                    //}
                    else if(additionFinished)
                    {
                        break;
                    }
                }
            }                    
        }

        public static void Main(string[] args)
        {
            string data;
            Container container = new Container();
            using (var reader = new StreamReader("IFU-9_ZherdeivM_L1_dat_1.json"))
            {
                data = reader.ReadToEnd();
                container = JsonConvert.DeserializeObject<Container>(data);
            }
            int size = container.People.Count;
            DataMonitor dataM = new DataMonitor(size / 2);
            ResultMonitor resultM = new ResultMonitor(size);

            Console.WriteLine(data);
            Console.WriteLine(size);

            var threads = Enumerable.Range(0, 4)
            .Select((x) => new Thread(() => Working(dataM, resultM)))
            .ToList();



            threads.ForEach(x => x.Start());

            //start threads
            foreach (Person item in container.People)
            {
                dataM.AddItem(item);
            }
            additionFinished = true;
            // Wait for all created threads to finish 
            threads.ForEach(x => x.Join());


            resultM.WriteAllItemsToFile();



        }

    }
}
