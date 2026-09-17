using StudentApi.Models;
using System;
using BCrypt.Net;

namespace StudentApi.DataSimulation
{
    public class StudentDataSimulation
    {

        // Static list of students, acting as an in-memory data store, you can change it later on to retrieve students from Database.
        public static readonly List<Student> StudentsList= new List<Student>
        {
            // Initialize the list with some student objects.
            new Student { Id = 1, Name = "Ali Ahmed", Age = 20,Grade=88 ,Email="ali@gmail.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Ali123") , Role="Student"},
            new Student { Id = 2, Name = "Fadi Khail", Age = 22,Grade=77 ,Email="Ahmed@gmail.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Ahmed123"), Role="Admin"},
            new Student { Id = 3, Name = "Ola Jaber", Age = 21 , Grade = 66, Email="Ola@gmail.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Ola123"), Role="Student"},
            new Student { Id = 4, Name = "Alia Maher", Age = 19,Grade=44, Email="Alia@gmail.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Alia123"), Role="Admin" },
            new Student { Id = 5, Name = "Hala Jaber", Age = 23,Grade=99, Email="Hala@gmail.com", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Ali123"), Role="Student" }
        };

    }
}
