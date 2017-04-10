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
        static byte[] bigBlock;

        /// <summary>
        /// Allocates 90,000 byte blocks, optionally intersperced with larger blocks
        /// </summary>
        static void Fill(bool allocateBigBlocks, bool grow, bool alwaysGC)
        {
            // Number of bytes in a small block
            // 90000 bytes, just above the limit for the LOH
            const int blockSize = 90000;

            // Number of bytes in a larger block: 16Mb initially
            int largeBlockSize = 1 << 24;

            // Number of small blocks allocated
            int count = 0;

            try
            {
                // We keep the 'small' blocks around 
                // (imagine an algorithm that allocates memory in chunks)
                List<byte[]> smallBlocks = new List<byte[]>();

                for (;;)
                {
                    // Write out some status information
                    //if ((count % 1000) == 0)
                    //{
                    //    Console.CursorLeft = 0;
                    //    Console.Write(new string(' ', 20));
                    //    Console.CursorLeft = 0;
                    //    Console.Write("{0}", count);
                    //    Console.CursorLeft = 0;
                    //}

                    // Force a GC if necessaryry
                    if (alwaysGC) GC.Collect();

                    // Allocate a larger block if we're set up to do soso
                    if (allocateBigBlocks)
                    {
                        bigBlock = new byte[largeBlockSize];
                    }

                    // The next 'large' block will be just slightly largerer
                    if (grow) largeBlockSize++;

                    // Allocate a new block
                    smallBlocks.Add(new byte[blockSize]);
                    count++;
                }
            }
            catch (OutOfMemoryException)
            {
                // Force a GC, which should empty the LOH again
                bigBlock = null;
                GC.Collect();

                // Display the results for the amount of memory we managed to allocate
               /* Console.WriteLine("{0}: {1}Mb allocated"
                                  , (allocateBigBlocks ? "With large blocks" : "Only small blocks")
                                  + (alwaysGC ? ", frequent garbage collections" : "")
                                  + (grow ? "" : ", large blocks not growing")
                                  , (count * blockSize) / (1024 * 1024));*/
            }
        }

        public ActionResult HighMemory()
        {
            Fill(true, true, false);
            Fill(true, true, true);
            Fill(false, true, false);
            Fill(true, false, false);

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