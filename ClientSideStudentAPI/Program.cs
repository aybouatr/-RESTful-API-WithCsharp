using System.Net.Http.Json;
using System;
using System.Text;

namespace ClientSideStudentAPI
{
    class Program
    {
        static HttpClient HttpClient = new HttpClient();

        

        static async Task Main(string[] args)
        {

            HttpClient.BaseAddress = new Uri("https://localhost:7021/api/StudentsAPI/");

            await FetchStudentsAsync();
            await FetchPassedStudentsAsync();
            await FetchStudentById();
            await FetchAvergeGrade();
            await FetchAddStudentsAsync();
           


        }

        static async Task FetchAddStudentsAsync()
        {
            {
                Console.WriteLine("==================== Adding New Student ====================\n");
                var newStudent = new Student
                {
                    Id = 5,
                    FullName = "John Doe",
                    Age = 24,
                    Grade = 88
                };
                try
                {
                    var response = await HttpClient.PostAsJsonAsync("AddStudent", newStudent);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("New student added successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to add student. Status Code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
           
        }

        static async Task FetchAvergeGrade()
        {
            Console.WriteLine("==================== Average Grade of All Students ====================\n");
            try
            {
                var response = await HttpClient.GetFromJsonAsync<double>("AverageGrade");
                Console.WriteLine($"Average Grade: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task FetchStudentsAsync()
        {
            Console.WriteLine("==================== All Students ====================\n");

            try
            {
                var response = await HttpClient.GetFromJsonAsync<List<Student>>("Students");

                if (response != null)
                {
                    foreach (var student in response)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.FullName}, Grade: {student.Grade}, Age : {student.Age}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        static async Task FetchPassedStudentsAsync()
        {
            Console.WriteLine("==================== Students Passed Grade 80 ====================\n");
            try
            {
                var response = await HttpClient.GetFromJsonAsync<List<Student>>("Pass");
                if (response != null)
                {
                    foreach (var student in response)
                    {
                        Console.WriteLine($"ID: {student.Id}, Name: {student.FullName}, Grade: {student.Grade}, Age: {student.Age}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }


        }

        static async Task FetchStudentById()
        {
            Console.WriteLine("==================== Student with ID 1 ====================\n");
            try
            {
                var response = await HttpClient.GetFromJsonAsync<Student>("Student/1");
                if (response != null)
                {
                    Console.WriteLine($"ID: {response.Id}, Name: {response.FullName}, Grade: {response.Grade}, Age: {response.Age}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public class Student
        {
            public int Id { get; set; }

            public string FullName { get; set; }

            public int Grade { get; set; }

            public int Age { get; set; }

        }

    }
}
