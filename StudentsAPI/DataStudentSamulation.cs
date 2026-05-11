using System;
using System.Xml.Linq;
using StudentsAPI.Model;

namespace DataSimulation
{
    public class DataStudentSamulation
    {
        public static readonly List<Student> Students = new List<Student>
        {
            new Student { Id = 1, FullName = "Ali", Age = 20, Grade = 85 },
            new Student { Id = 2, FullName = "Sara", Age = 22, Grade = 90 },
            new Student { Id = 3, FullName = "Yassine", Age = 21, Grade = 78 },
            new Student { Id = 4, FullName = "Omar", Age = 23, Grade = 92 }
        };
    }
}
