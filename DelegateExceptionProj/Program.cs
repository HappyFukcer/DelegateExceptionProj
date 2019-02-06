using System;
using System.Collections.Generic;

namespace DelegateExceptionProj
{
    public delegate void customDelegate(string msg);
    #region not working
    class DelEvent
    {

        customDelegate Del;

        event customDelegate Eve;
        public void AddDel(customDelegate del)
        {
            Del += del;
            Eve += del;
        }

        public void TriggerDel()
        {
            //foreach (var del in Eve.GetInvocationList())
            try
            {
                //del.("Delegate call");
                Del("Delegate call");
            }
            catch (Exception e)
            {
                Console.WriteLine("Cathed Error!!!");

            };
        }

    }
    #endregion

    class DelCollection
    {
        List<customDelegate> DelList { get; set; } = new List<customDelegate>();

        public void AddDel(customDelegate del)
        {
            DelList.Add(del);
        }

        public void TriggerDel()
        {
            foreach (var del in DelList)
            {
                try {
                    del("Delegate call");
                }
                catch {
                    Console.WriteLine("Cathed Error!!!");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region not working
            //Car car = new Car();

            //for (int i = 0; i < 5; i++)
            //    car.AddDel((msg) => Console.WriteLine(msg));

            //car.AddDel((msg) => throw new Exception("Error!!!"));

            //for (int i = 0; i < 5; i++)
            //    car.AddDel((msg) => Console.WriteLine(msg));

            //car.TriggerDel();
            #endregion

            DelCollection delCollection = new DelCollection();

            for (int i = 0; i < 5; i++)
                delCollection.AddDel((msg) => Console.WriteLine(msg));

            delCollection.AddDel((msg) => throw new Exception("Error!!!"));

            for (int i = 0; i < 5; i++)
                delCollection.AddDel((msg) => Console.WriteLine(msg));

            delCollection.TriggerDel();

            Console.WriteLine("End of delegates!");

            Console.ReadLine();

            List<customDelegate> localDelCollection = new List<customDelegate>();

            //question on interview
            for (int i = 0; i < 15; i++)
                localDelCollection.Add((msg) => Console.WriteLine(i));

            foreach(var del in localDelCollection)
            {
                del("");
            }
            Console.ReadLine();

        }
    }
}
