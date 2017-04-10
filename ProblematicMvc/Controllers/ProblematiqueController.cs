using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace ProblematicMvc.Controllers
{
    public class ProblematiqueController : Controller
    {
        // GET: Problematique
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HighCpu(int num = 1)
        {
            string LargeString = "Historically, the world of data and the world of objects " +
            "have not been well integrated. Programmers work in C# or Visual Basic " +
            "and also in SQL or XQuery. On the one side are concepts such as classes, " +
            "objects, fields, inheritance, and .NET Framework APIs. On the other side " +
            "are tables, columns, rows, nodes, and separate languages for dealing with " +
            "them. Data types often require translation between the two worlds; there are " +
            "different standard functions. Because the object world has no notion of query, a " +
            "query can only be represented as a string without compile-time type checking or " +
            "IntelliSense support in the IDE. Transferring data from SQL tables or XML trees to " +
            "objects in memory is often tedious and error-prone.";
            string SmallText = ".................";
            for (int i = 0; i < 1000; i++)
            {
                LargeString += SmallText;
            }
            ViewBag.Message = LargeString;
            ViewBag.Num = num * 1000;
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

        /// Static variable used to store our 'big' block. This ensures that the block is always up for garbage collection.
        /// </summary>
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