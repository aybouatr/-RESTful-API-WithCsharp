using Microsoft.Data.SqlClient;
using SudentsDataAccessLayer;
using System;
using System.Data;
using System.Reflection.PortableExecutable;

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
            catch (Exception)
            {
                avg = -1;
                //Console.WriteLine(ex.Message);
            }

            return avg;
        }


        public static StudentOTO GetStudentById(int id)
        {
            try
            {
              
                using (SqlConnection connection = new SqlConnection(SettingStringConnections.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetStudentById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                      
                        cmd.Parameters.AddWithValue("@StudentId", id);

                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            
                            if (reader.Read())
                            {
                                return new StudentOTO(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetInt32(reader.GetOrdinal("Age")),
                                    reader.GetInt32(reader.GetOrdinal("Grade"))
                                );
                            }
                        }
                    }
                }

                return null; // no data found
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // better for debugging
                return null;
            }
        }


        public static int AddNewStudent(StudentOTO student)
        {
            int id = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(SettingStringConnections.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AddStudent", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Name", student.FullName);
                        cmd.Parameters.AddWithValue("@Age", student.Age);
                        cmd.Parameters.AddWithValue("@Grade", student.Grade);

                        SqlParameter outputId = new SqlParameter("@NewStudentId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputId);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        id = (int)outputId.Value;
                    }
                }
            }
            catch (Exception)
            {
               id = -1;
            }

            return id;
        }

    }

}
