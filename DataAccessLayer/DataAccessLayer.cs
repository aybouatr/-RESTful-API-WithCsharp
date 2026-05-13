using System;
using System.Data;
using Microsoft.Data.SqlClient;
using SudentsDataAccessLayer;

namespace DataAccessLayer
{
    public class StudentOTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }

        public StudentOTO(int  id, string fullName, int age, double grade)
        {
            Id = id;
            FullName = fullName;
            Age = age;
            Grade = grade;
        }
    }

    public class DataAccessLayer
    {
       // public static string ConnectionString { get; set; } = "Data Source=.;Initial Catalog=StudentDB;Integrated Security=True";

        public static List<StudentOTO> GetAllStudent()
        {
            var students = new List<StudentOTO>();

            using (SqlConnection connection = new SqlConnection(SettingStringConnections.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                          
                            students.Add(new StudentOTO(

                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }

            }

            return students;

        }
            
        public static List<StudentOTO> GetAllStudentPassed()
        {
            var students = new List<StudentOTO>();
            using (SqlConnection connection = new SqlConnection(SettingStringConnections.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPassedStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new StudentOTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }
            return students;
        }


        public static double GetAvg()
        {
            double avg = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(SettingStringConnections.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAverageGrade", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            avg = Convert.ToDouble(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // You can log the error here
                Console.WriteLine(ex.Message);
            }

            return avg;
        }




    }



}
