using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace LayerBusnessLogic
{
    public class BusnisseLogicLayer
    {

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

    }
}
