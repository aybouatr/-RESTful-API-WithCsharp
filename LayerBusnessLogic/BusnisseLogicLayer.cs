using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DataAccessLayer;

namespace LayerBusnessLogic
{
    public class Student
    {

        public enum enMode { AddNewStudent,UpdateStudent};

        public enMode Mode = enMode.AddNewStudent;

        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }


        public Student(StudentOTO student,enMode mode = enMode.AddNewStudent)
        {
            this.Id = student.Id;
            this.FullName = student.FullName;
            this.Age = student.Age;
            this.Grade = student.Grade;
            this.Mode = mode;
        }

        private Student()
        {
            this.Id = -1;
            this.FullName = string.Empty;
            this.Age = -1;
            this.Grade =-1;
            this.Mode = enMode.AddNewStudent;
        }

        public StudentOTO SDTO()
        {
            return new StudentOTO(this.Id,this.FullName,this.Age,this.Grade);
        }

        static public List<StudentOTO> GetAllStudents()
        {
            return DataAccessLayer.DataAccessLayer.GetAllStudent(); 
        }

        static public List<StudentOTO> GetPassStudent()
        {
            return DataAccessLayer.DataAccessLayer.GetAllStudentPassed();
        }


        static public double GetAvrg()
        {
            return DataAccessLayer.DataAccessLayer.GetAvg();
        }

        public static Student Find(int  id)
        {
           
            StudentOTO studentDTO = DataAccessLayer.DataAccessLayer.GetStudentById(id);
            if (studentDTO == null)
            {
                return null;
            }    
            return new Student(studentDTO,enMode.UpdateStudent); ;
        }

        private bool _AddNewStudent()
        {
            StudentOTO s = new StudentOTO(this.Id,this.FullName,this.Age, this.Grade);
            int id = DataAccessLayer.DataAccessLayer.AddNewStudent(s);
            this.Id = id;
            
            return (id != -1);
        }

        private bool _UpdateInfoStudent()
        {


            return false;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNewStudent:
                    bool result = _AddNewStudent();
                    if (result)
                        this.Mode = enMode.UpdateStudent;
                    return result;
                case enMode.UpdateStudent:
                    return _UpdateInfoStudent();
            }
            return false;

        }

    }
}
