using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace DemoWithAPIThreeTierAppWithDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPI : ControllerBase
    {

        [HttpGet("AllStudents", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<StudentOTO>> GetAllStudents()
        {
            var students = LayerBusnessLogic.BusnisseLogicLayer.GetAllStudents();
            if (students == null || !students.Any())
            {
                return NotFound("No students found");
            }
            return Ok(students);

        }


        [HttpGet("AllPassStudents" , Name = "GetAllPassStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<StudentOTO>> GetAllPassStudent()
        {
            var Students = DataAccessLayer.DataAccessLayer.GetAllStudentPassed();
            if (Students == null || !Students.Any())
            {
                return NotFound("No students found");
            }

            return Ok(Students);
        }


        [HttpGet("Avrg",Name = "GetAvrg")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public double GetAvrg()
        {
            return LayerBusnessLogic.BusnisseLogicLayer.GetAvrg();
        }

    }
}
