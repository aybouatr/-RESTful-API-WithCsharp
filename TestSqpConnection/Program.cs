using System;
using DataAccessLayer;

namespace TestSqpConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing SQL Connection...");
            
            List<StudentOTO> students = DataAccessLayer.DataAccessLayer.GetAllStudent();
            if (students == null || !students.Any())
            {
                Console.WriteLine("No students found");
            }
            else
            {
                Console.WriteLine("Students found:");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.Id}, Name: {student.FullName}");
                }
            }

        }
    }
}


