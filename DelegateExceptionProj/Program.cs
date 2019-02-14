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
            foreach (var del in Eve.GetInvocationList())
                try
                {
                    //looks good!!!! but not good enough
                    del.Method.Invoke("Delegate call, say Hello", null);
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
                try
                {
                    del("Delegate call");
                }
                catch
                {
                    Console.WriteLine("Cathed Error!!!");
                }
            }
        }
    }

    static class DelegateHelper
    {
        public static void ExceptionSafe(this MulticastDelegate target, params object[] args)
        {
            if (target == null) return;
            foreach (var del in target.GetInvocationList())
            {
                try
                {
                    del.DynamicInvoke(args);
                }
                catch
                {
                    Console.WriteLine("Exception caught!");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region  question on interview
            //List<customDelegate> localDelCollection = new List<customDelegate>();

            //for (int i = 0; i < 15; i++)
            //    localDelCollection.Add((msg) => Console.WriteLine(i));

            //foreach (var del in localDelCollection)
            //{
            //    del("");
            //}
            #endregion

            #region not working
            //DelEvent car = new DelEvent();

            //for (int i = 0; i < 5; i++)
            //    car.AddDel((msg) => Console.WriteLine(msg));

            //car.AddDel((msg) => throw new Exception("Error!!!"));

            //for (int i = 0; i < 5; i++)
            //    car.AddDel((msg) => Console.WriteLine(msg));

            //car.TriggerDel();
            #endregion
            
            #region not exactly
            //DelCollection delCollection = new DelCollection();

            //for (int i = 0; i < 5; i++)
            //    delCollection.AddDel((msg) => Console.WriteLine(msg));

            //delCollection.AddDel((msg) => throw new Exception("Error!!!"));

            //for (int i = 0; i < 5; i++)
            //    delCollection.AddDel((msg) => Console.WriteLine(msg));

            //delCollection.TriggerDel();

            //Console.WriteLine("End of delegates!");

            #endregion

            #region the right way
            customDelegate dels = null;
            for (int i = 0; i < 5; i++)
                dels += ((msg) => Console.WriteLine(msg));

            //dels += ((msg) => throw new Exception("Error!!!"));
            dels += Exception;
            for (int i = 0; i < 5; i++)
                dels += ((msg) => Console.WriteLine(msg));

            dels.ExceptionSafe(new object[] { "Hello!!" });
            #endregion

            Console.ReadLine();

        }

        static void SomeMessage(object p)
        {

            Console.WriteLine(p);
        }

        static void Exception(object p)
        {
            throw new Exception(p.ToString());
        }
    }
}
