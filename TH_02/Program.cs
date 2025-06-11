using System;
using System.Threading;

class Program
{
    static int size = 100;
    static int[] buffer = new int[size];

    static int inBuf = 0;
    static int outBuf = 0;
    static int count = 0;

    static object lockObj = new object();

    static Semaphore isEmpty = new Semaphore(0, size);
    static Semaphore isFull = new Semaphore(2, size);

    static Random random = new Random();

    static void Producer(object id)
    {
        while (true)
        {
            int value;

            lock (random)
            {
                value = random.Next(1, 100);
            }

            isEmpty.WaitOne();

            lock (lockObj)
            {
                buffer[inBuf] = value;
                inBuf = (inBuf + 1) % size;
                count++;
                Console.WriteLine($"P{id}: {value} - {DateTime.Now}");
            }
            Thread.Sleep(3000);
            isFull.Release();
        }
    }

    static void Consumer(object id)
    {
        while (true)
        {
            isFull.WaitOne();

            int value;
            int max = int.MinValue;

            lock (lockObj)
            {
                value = buffer[outBuf];
                outBuf = (outBuf + 1) % size;
                count--;

                for (int i = 0, idx = outBuf; i < count; i++)
                {
                    if (buffer[idx] > max)
                        max = buffer[idx];
                    idx = (idx + 1) % size;
                }

                if (count == 0)
                    max = value;
            }

            Console.WriteLine($"C{id}: {value} - {max} - {DateTime.Now}");
            Thread.Sleep(3000);
            isEmpty.Release();
        }
    }

    static void Main(string[] args)
    {
        int k = 3;
        int h = 2;

        for (int i = 1; i <= k; i++)
        {
            Thread p = new Thread(Producer);
            p.Start(i);
        }

        for (int i = 1; i <= h; i++)
        {
            Thread c = new Thread(Consumer);
            c.Start(i);
        }
    }
}