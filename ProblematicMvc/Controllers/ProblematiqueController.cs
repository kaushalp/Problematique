using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;
using System.Diagnostics;
using System.Threading;

namespace ProblematicMvc.Controllers
{
    public class ProblematiqueController : Controller
    {
        // GET: Problematique
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HighCpu(int percentage = 99)
        {
            //Source: http://stackoverflow.com/questions/2514544/simulate-steady-cpu-load-and-spikes 
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("percentage");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                // Make the loop go on for "percentage" milliseconds then sleep the 
                // remaining percentage milliseconds. So 40% utilization means work 40ms and sleep 60ms
                if (watch.ElapsedMilliseconds > percentage)
                {
                    Thread.Sleep(100 - percentage);
                    watch.Reset();
                    watch.Start();
                }
            }

            return View();
        }
        public ActionResult Crash()
        {
            try
            {
                throw new System.Exception("There was a problem processing this request");
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
            return View();
        }
        public static void WriteLog(string message, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine("--------------------------");
                    sw.WriteLine(DateTime.Now.ToLongTimeString());
                    sw.WriteLine("-------------------");
                    sw.WriteLine("Error Occurred");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogException(ex);
            }
           // return View();
        }
        static byte[] byteArray;
        public ActionResult HighMemory()
        {
            try
            {
                // Allocating blocks larger than 85k (87040 bytes) so that it goes to LOH
                List<byte[]> sBlocks = new List<byte[]>();
                const int sBlkSize = 90000;

                // Allocating Larger blocks in LOH
                int lBlocks = 1 << 24;
                int count = 0;
                for(;;) // Infinite loop
                {
                    byteArray = new byte[lBlocks];
                    sBlocks.Add(new byte[sBlkSize]);
                    lBlocks++;
                    count++;
                }
            }
            catch
            {
            }
            //catch (OutOfMemoryException)
            //{
            //    // Force a GC
            //    //byteArray= null;
            //    GC.Collect();
            //}
            return View();
        }
    }
    internal class ExceptionHandler
    {
        public static void LogException(Exception ex)
        {
            ProblematiqueController.WriteLog(ex.Message, "D:\\log.txt");
        }
    }
}