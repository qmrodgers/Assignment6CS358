//created by Quaid Rodgers
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
            //Class ReadFile lets us perform this on multiple files without getting too complex
            ReadFile studentFile = new ReadFile("Students.txt");

            studentFile.addStudent("Malachi", "None", "Constant", "5675431823", "fakeemail@hotmail.com", 4.0);

            studentFile.highGPACount();

            studentFile.LastNames("Anderson");

            studentFile.countEmptyEmails();

            studentFile.listAverageGPA();

            studentFile.studentCount();

            //studentFile.listFile();

            studentFile.closeOutput();

            Console.ReadKey();
        }


    }

    public class ReadFile
    {
        private StreamWriter output;
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

            if (!File.Exists("Output.txt"));
            {
                File.Create("Output.txt").Dispose();
            }
            output = new StreamWriter("Output.txt");
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

            if (!File.Exists("Output.txt")) ;
            {
                File.Create("Output.txt").Dispose();
            }
            output = new StreamWriter("Output.txt");


        }
        public void closeOutput()
        {
            output.Close();
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

        public void studentCount()
        {
            int count = 0;
            string[] lineValues = GetValue();
            while (lineValues[0] != "QUIT")
            {
                count++;
                lineValues = GetValue();
            }
            Console.Write($"There are {count} total students.");
            output.WriteLine($"There are {count} total students.");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.WriteLine(".");
            Thread.Sleep(300);
            inFile = new StreamReader(fileLocation);
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
            sub = inLine;
            
            sub = sub.Substring(inLine.IndexOf("'") + 1);
            // values[2] = last name
            values[2] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf(" ") + 2);
            // values[0] = first name
            values[0] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf(" ") + 2);
            // values[1] = initial
            values[1] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf("'") + 1);
            // values[3] = Phone
            values[3] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf(" ") + 2);
            // values[4] = email
            values[4] = sub.Substring(0, sub.IndexOf(" "));
            sub = sub.Substring(sub.IndexOf(" ") + 1);
            // values[5] = GPA
            values[5] = sub.Substring(0, sub.IndexOf(" "));


            return values;
        }


        //generic method (not part of assignment) to list all last names in the list.
        public void LastNames()
        {
            string[] lineValues = GetValue();
            while (lineValues[0] != "QUIT")
            {

                Console.WriteLine(lineValues[2]);
                lineValues = GetValue();
                
            }





            inFile = new StreamReader(fileLocation);
        }

        //Lists the count of people with a given last name.
        public void LastNames(string lastname)
        {
            int count = 0;
            string[] lineValues = GetValue();
            while (lineValues[2] != "QUIT")
            {
                if(lineValues[2] == lastname)
                {
                    count++;
                }
                lineValues = GetValue();

            }

            Console.Write($"There are {count} people with the last name {lastname}");
            output.WriteLine($"There are {count} people with the last name {lastname}.");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.WriteLine(".");
            Thread.Sleep(300);




            inFile = new StreamReader(fileLocation);
        }


        //Lists a count of all people with a GPA of 3.0 or above
        public void highGPACount()
        {
            string[] lineValues = GetValue();
            int count = 0;


            while (lineValues[5] != "QUIT")
            {
                try
                  {
                    if (Double.Parse(lineValues[5]) >= 3.0)
                    {
                        count++;  
                    }

                    lineValues = GetValue();
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception in highGPACount, something in the document violates formatting rules.");
                    break;
                }
            }




            Console.Write($"The total number of students with a 3.0 or above GPA is: {count}");
            output.WriteLine($"The total number of students with a 3.0 or above GPA is: {count}.");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.WriteLine(".");
            Thread.Sleep(300);

            inFile = new StreamReader(fileLocation);
        }

        public double averageGPA()
        {
            string[] lineValues = GetValue();
            double average = 0;
            int count = 0;
            double sum = 0;

            while (lineValues[5] != "QUIT")
            {
                try
                {
                    sum = sum + Double.Parse(lineValues[5]);
                    count++;

                    lineValues = GetValue();
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception in highGPACount, something in the document violates formatting rules.");
                    break;
                }
            }

            average = sum / count;
            inFile = new StreamReader(fileLocation);

            return average;
        }
        public void listAverageGPA()
        {
            Console.Write($"Average GPA is {averageGPA()}");
            output.WriteLine($"Average GPA is {averageGPA()}");


            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.WriteLine(".");
            Thread.Sleep(300);
        }

        //extra method to grab email counts
        private int emailAddresses(string email = null)
        {
            string[] lineValues = GetValue();
            int count = 0;
            if (email == null)
            {
                while (lineValues[2] != "QUIT")
                {
                    count++;
                    lineValues = GetValue();

                }
                inFile = new StreamReader(fileLocation);
                return count;
            }
            else
            {
                while (lineValues[2] != "QUIT")
                {
                    if (lineValues[4] == email) count++;
                    lineValues = GetValue();


                }
                inFile = new StreamReader(fileLocation);
                return count;
            }
        }


        public void countEmptyEmails()
        {
            Console.Write($"There are {emailAddresses("NONE")} students with no email");
            output.WriteLine($"There are {emailAddresses("NONE")} students with no email.");

            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.Write(".");
            Thread.Sleep(300);
            Console.WriteLine(".");
            Thread.Sleep(300);
        }
    }
}
