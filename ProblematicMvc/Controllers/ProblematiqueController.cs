using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;

namespace ProblematicMvc.Controllers
{
    public class ProblematiqueController : Controller
    {
        // GET: Problematique
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HighCpu(int num = 10)
        {
            var fileContents = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/big.txt"));
            string result = fileContents;
            for(int i=0;i<num;i++)
            {
                result = string.Concat(result, result);
            }
            ViewBag.Message = result;
            //ViewBag.Num = num * 1000;
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
            catch (OutOfMemoryException)
            {
                // Force a GC
                //byteArray= null;
                GC.Collect();
            }
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