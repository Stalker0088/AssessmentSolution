using System;

namespace StudentMarksApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Student Marks Application";

            Console.WriteLine("=======================================");
            Console.WriteLine("        STUDENT MARKS APPLICATION      ");
            Console.WriteLine("=======================================");
            Console.WriteLine();

            Console.Write("Enter student name: ");
            string studentName = Console.ReadLine();

            double mark1 = GetValidMark("Enter mark for Subject 1: ");
            double mark2 = GetValidMark("Enter mark for Subject 2: ");
            double mark3 = GetValidMark("Enter mark for Subject 3: ");

            double total = mark1 + mark2 + mark3;
            double average = total / 3;

            string result = average >= 50 ? "PASS" : "FAIL";

            Console.WriteLine();
            Console.WriteLine("=======================================");
            Console.WriteLine("             STUDENT RESULT            ");
            Console.WriteLine("=======================================");
            Console.WriteLine($"Student Name : {studentName}");
            Console.WriteLine($"Subject 1    : {mark1}");
            Console.WriteLine($"Subject 2    : {mark2}");
            Console.WriteLine($"Subject 3    : {mark3}");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Total Marks  : {total}");
            Console.WriteLine($"Average      : {average:F2}");
            Console.WriteLine($"Result       : {result}");
            Console.WriteLine("=======================================");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static double GetValidMark(string message)
        {
            double mark;

            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();

                if (double.TryParse(input, out mark))
                {
                    if (mark >= 0 && mark <= 100)
                    {
                        return mark;
                    }
                    else
                    {
                        Console.WriteLine("Invalid mark. Please enter a value between 0 and 100.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
            }
        }
    }
}