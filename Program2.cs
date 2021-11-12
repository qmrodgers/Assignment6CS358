/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication61
{
    class Program
    {
        static void Main(string[] args)
        {

            StreamReader inFile;
            string inLine;

            if (File.Exists("Students.txt"))
            {
                try
                {
                    inFile = new StreamReader("Students.txt");
                    while ((inLine = inFile.ReadLine()) != null)
                    {
                        int start = inLine.IndexOf("'");
                        if (start >= 0)
                        {
                            inLine = inLine.Substring(start);
                            int end = inLine.IndexOf(" ");
                            string lastname = inLine.Substring(0, end);

                            Console.WriteLine(lastname);
                        }
                    }
                }
                catch (System.IO.IOException exc)
                {
                    Console.WriteLine("Error");
                }

            }
            Console.ReadKey();
        }
    }
}
*/
