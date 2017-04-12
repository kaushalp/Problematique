using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HighCpuSimulation
{
    class Program
    {
        public static void CPUKill(object cpuUsage)
        {
            Parallel.For(0, 1, new Action<int>((int i) =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                while (true)
                {
                    // Make the loop go on for "percentage" milliseconds then sleep the 
                    // remaining percentage milliseconds. So 40% utilization means work 40ms and sleep 60ms
                    if (watch.ElapsedMilliseconds > (int)cpuUsage)
                    {
                        Thread.Sleep(100 - (int)cpuUsage);
                        watch.Reset();
                        watch.Start();
                    }
                }
            }));
        }

        static void Main(string[] args)
        {
            int cpuUsage = 75;
            int time = 30000;
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i <=Environment.ProcessorCount; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(CPUKill));
                t.Start(cpuUsage);
                threads.Add(t);
            }
            Thread.Sleep(time);
            foreach (var t in threads)
            {
                t.Abort();
            }

        }

    }
}
