using System;
using System.Threading;
// три потока соревнуются, кто быстрее завершит работу
public class MainClass
{
    public static void Main()
    {
        Console.WriteLine("Основной поток начат.");


        var mt1 = new ThreadClass("Поток #1");
        var mt2 = new ThreadClass("Поток #2");
        var mt3 = new ThreadClass("Поток #3");

        var thread1 = new Thread(mt1.Run);
        var thread2 = new Thread(mt2.Run);
        var thread3 = new Thread(mt3.Run);

        thread1.Start();
        thread2.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        Console.WriteLine("Основной поток завершен.");
    }

    class ThreadClass
    {
        public int Count;
        string threadName;
        Random rand = new Random();

        public ThreadClass(string name)
        {
            Count = 0;
            threadName = name;
        }

        public void Run()
        {
            Console.WriteLine(threadName + " начат.");
            do{
                // Синхронизируем доступ к объекту Random
                int sleepTime;
                lock (rand)
                {
                    sleepTime = rand.Next(100, 500); // Случайное время ожидания
                }

                Thread.Sleep(sleepTime);
                Console.WriteLine(threadName + " счетчик = " + Count + " (задержка: " + sleepTime + "мс)");
                Count++;
            } 
            while (Count < 3);

            Console.WriteLine(threadName + " завершен.");
            Console.ReadKey();
        }
    }
}
