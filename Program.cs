using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleApplication61
{
    class Program
    {
        static void Main(string[] args)
        {

            ReadFile studentFile = new ReadFile("Students.txt");


            studentFile.listLastNames();

            studentFile.highGPACount();

            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.WriteLine(".");
            Thread.Sleep(300);
            Thread.Sleep(4000);

            studentFile.listFile();

            //studentFile.addStudent("Malachi", "None", "Constant", "5675431823", "fakeemail@hotmail.com", 4.0);
            Thread.Sleep(2000);

            studentFile.listFile();
            Console.ReadKey();
        }


    }

    public class ReadFile
    {
        private StreamReader inFile;
        private string fileLocation;
        private string directory;
        private string inLine;


        // Constructors that I put too much effort into for some reason
        public ReadFile()
        {
            bool Reset = true;
            string temp = "";
            do
            {
                Console.Write("Please provide file location to read (Default is Debug Folder): ");
                temp = Console.ReadLine();
                try
                {
                    inFile = new StreamReader(temp);
                    Reset = false;

                }
                catch (FileNotFoundException)
                {
                    Console.Write("File Not Found");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.WriteLine(".");
                    Thread.Sleep(300);

                    Reset = true;
                }
            }
            while (Reset);

            fileLocation = temp;
            findDirectory(fileLocation);

            Console.WriteLine($"File Read Successfully: {temp}");
        }
        public ReadFile(string fileLocationPara)
        {
            string temp = "";
            bool Reset = true;
            try
            {
                inFile = new StreamReader(fileLocationPara);
                temp = fileLocationPara;
            }
            catch (FileNotFoundException)
            {
                do
                {
                    Console.Write("File Not Found");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.Write(".");
                    Thread.Sleep(300);
                    Console.WriteLine(".");
                    Thread.Sleep(300);
                    Console.Write("Please provide file location to read (Default is Debug Folder): ");
                    temp = Console.ReadLine();

                    try
                    {
                        inFile = new StreamReader(temp);
                        Reset = false;
                    }
                    catch (FileNotFoundException)
                    {
                        Reset = true;
                    }
                }
                while (Reset);
            }
            fileLocation = temp;
            findDirectory(fileLocation);
            Console.WriteLine($"File Read Successfully: {temp}");


        }

        private void findDirectory(string path)
        {
            string[] split;
            if (path.IndexOf(@"/") != -1 && path.IndexOf(@"\") != -1)
            {
                if (path.IndexOf(@"/") > path.IndexOf(@"\")) directory = path.Substring(0, path.LastIndexOf(@"/") + 1);
                else directory = path.Substring(0, path.LastIndexOf(@"\") + 1);
            }
            else directory = "";
        }

        public void listFile()
        {


            //Does same thing
            Console.WriteLine(inFile.ReadToEnd());
            inFile = new StreamReader(fileLocation);

            Console.WriteLine("Done listing file");
            Thread.Sleep(3000);
        }

        public void addStudent(string FirstName, string Initial, string LastName, string PhoneNo, string Email, double GPA)
        {
            string currName;
            string tempFileLoc = directory + "temp.txt";
            if (!File.Exists(tempFileLoc))
            {
                File.Create(tempFileLoc).Dispose();
            }
            StreamWriter writeFile = new StreamWriter(tempFileLoc);
            inLine = inFile.ReadLine();
            writeFile.WriteLine(inLine);
            while ((inLine = inFile.ReadLine()) != null)
            {
                currName = inLine.Substring(inLine.IndexOf("'"), inLine.IndexOf("'") + inLine.Substring(inLine.IndexOf("'")).IndexOf(" "));
                if (String.Compare(LastName, currName) < 0)
                {
                    break;
                }
                writeFile.WriteLine(inLine);
            }
            writeFile.WriteLine($"(LIST (LIST '{LastName} '{FirstName} '{Initial} ) '{PhoneNo} '{Email} {GPA} )");
            do
            {
                writeFile.WriteLine(inLine);
            }
            while ((inLine = inFile.ReadLine()) != null);
            writeFile.Close();
            inFile.Close();
            File.Delete(fileLocation);
            File.Move(tempFileLoc, fileLocation);

            inFile = new StreamReader(fileLocation);
        }
        public string[] GetValue()
        {
            string sub = "";
            string[] values = new string[6];

            inLine = inFile.ReadLine();
            if (inLine == "( students (LIST")
            {
                inLine = inFile.ReadLine();
            }
            else if (inLine == " ) )")
            {
                values[0] = "QUIT"; values[1] = "QUIT"; values[2] = "QUIT"; values[3] = "QUIT"; values[4] = "QUIT"; values[5] = "QUIT"; return values;
            }
            sub = inLine.Substring(inLine.IndexOf("'") + 1);
            // values[2] = last name
            values[2] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf("'") + 1);
            // values[0] = first name
            values[0] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf("'") + 1);
            // values[1] = initial
            values[1] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf("'") + 1);
            // values[3] = Phone
            values[3] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf("'") + 1);
            // values[4] = email
            values[4] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf(" ") + 1);
            // values[5] = GPA
            values[5] = sub.Substring(0, sub.IndexOf(" "));


            return values;
        }

        public void listLastNames()
        {
            string[] lineValues = GetValue();
            while (lineValues[0] != "QUIT")
            {

                Console.WriteLine(lineValues[2]);
                lineValues = GetValue();

            }





            inFile = new StreamReader(fileLocation);
        }

        public void highGPACount()
        {
            string[] lineValues = GetValue();
            int count = 0;


            while (lineValues[5] != "QUIT")
            {
                try
                {
                    if (Convert.ToDouble(lineValues[5]) >= 0)
                    {
                        count++;
                    }

                    lineValues = GetValue();
                }
                catch (Exception)
                {
                    break;
                }
            }




            Console.Write($"The total number of students with a 3.0 or above GPA is: {count}");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.WriteLine(".");
            Thread.Sleep(300);

            inFile = new StreamReader(fileLocation);
        }
    }
}
