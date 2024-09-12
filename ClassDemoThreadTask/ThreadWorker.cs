using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDemoThreadTask
{
    public class ThreadWorker
    {
        /*
         * Delegate
         */

        protected delegate void BeregnMetodeType(int x, int y); 



        public void StartDelegate()
        {
            BeregnMetodeType beregnMetode = (i, j) => { Console.WriteLine("Sum = " + (i + j)); };
            // anonym metode

            Console.WriteLine("step 1");
            beregnMetode(56, 9);


            Console.WriteLine("step 2");
            beregnMetode += Gange;
            beregnMetode(56, 9);

            Console.WriteLine("step 3");
            beregnMetode -= Gange;
            beregnMetode(1, 2);

        }

        private void Gange(int k, int l)
        {
            Console.WriteLine("Gange = " + (k * l));
        }
        







        /*
         * Threading
         */

        // what is example
        private static bool done = false; // Static fields are shared between all threads

        // sync med lock
        private object lockObj = new object();
        private Semaphore sem = new Semaphore(1, 1);

        public void ExampleThread()
        {
            new Thread(Go).Start();
            Go();
        }

        private void Go()
        {
            //lock (lockObj) { // kun een tråd inde ad gangen
            //    if (!done)
            //    {
            //        Thread.Sleep(1); done = true; Console.WriteLine("Done");
            //    }
            //}

            sem.WaitOne();
            if (!done)
            {
                Thread.Sleep(1); done = true; Console.WriteLine("Done");
            }
            sem.Release();

        }


        // show concurrency
        public void StartTaskTest()
        {
            Task t1 = Task.Run(() => DoWork("Number One", ConsoleColor.Red));
            Task t2 = Task.Run(() => DoWork("Number Two", ConsoleColor.Green));

            Thread.Sleep(1000);
            t1.Wait();
            t2.Wait();
        }

        private void DoWork(String name, ConsoleColor colour)
        {
            for (int i = 0; i < 30; i++)
            {
                Console.ForegroundColor = colour;
                Console.WriteLine($"Name {name} no={i}");
            }
        }



    }
}
