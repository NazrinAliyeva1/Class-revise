using System.Diagnostics;
using System.Timers;

namespace Paralellism_inClass
{
     class Program
    {
        static int Num;
        static object Aki = new object();
        static void Main(string[] args)
        {

            #region Concurrency learn
            Thread thread = new Thread(Loop1);
            Thread thread2 = new Thread(Loop2);
            thread.Start();
            thread.Join();
            thread2.Start();
            Console.WriteLine("jk");
            #endregion

            #region
            Parallel.Invoke(Loop1);
            Console.WriteLine("aliyeva");
            #endregion


            #region Race condition
            Thread thread3 = new Thread(Increase);
            Thread thread4 = new Thread(Decrease);
            thread4.Start();
            thread3.Start();
            thread4.Join();
            thread3.Join();
            Console.WriteLine(Num);
            #endregion

            #region Task
            SeherYemeyiAsync().Wait();
            #endregion

        }
        static async Task SeherYemeyiAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            //hamisini eyni anda yerine yetirir:
            await Task.WhenAll(PrepareTeaAsync(), BoilSausageAsync(), SufreHazirlaAsync());

            //await PrepareTeaAsync();
            //await BoilSausageAsync();
            //await SufreHazirlaAsync();
            Console.WriteLine(sw.ElapsedMilliseconds);

        }
        static async Task SufreHazirlaAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Sufre hazirdir.");
        }
        static async Task BoilSausageAsync()
        {
            await Task.Delay(5000);
            Console.WriteLine("Sausage Boiled");
        }
        static async Task PrepareTeaAsync()
        {
            Console.WriteLine("Caydani doldurduq");
            await Task.Delay(3000);
            Console.WriteLine("Su qaynadi, cay demlendi");
            Console.WriteLine("Cay hazirdi");
        }

        static void SeherYemeyi()
        {
            Stopwatch sw = Stopwatch.StartNew();
            PrepareTea();
            BoilSausage();
            SufreHazirla();
            Console.WriteLine(sw.ElapsedMilliseconds);

        }
        static void BoilSausage()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Sausage Boiled");
        }
        static void PrepareTea()
        {
            Console.WriteLine("Caydani doldurduq");
            Thread.Sleep(3000);
            Console.WriteLine("Su qaynadi, cay demlendi");
            Console.WriteLine("Cay hazirdi");
        }
        static void SufreHazirla()
        {
            Thread.Sleep(1200);
            Console.WriteLine("Sufre hazirdi");
        }

        static void Loop1()
        {
            for(int i = 0; i < 500; i++)
            {

                Console.WriteLine(i);
            }
        }
        static void Loop2()
        {
            for(int i=-500; i < 0; i++)
            {
                Console.WriteLine(i);
            }
        }
        static void Increase()
        {
            for(int i = 0; i < 10000000; i++)
                lock (Aki)

                    Num++;
            
        }
        static void Decrease()
        {
            for (int i = 0; i < 10000000; i++)
                lock (Aki)
                    Num--;
        }
    }
}
