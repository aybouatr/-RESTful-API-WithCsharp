using DataAccessLayer;
using LayerBusnessLogic;
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
            var students = LayerBusnessLogic.Student.GetAllStudents();
            if (students == null || !students.Any())
            {
                return NotFound("No students found");
            }
            return Ok(students);

        }


        [HttpGet("AllPassStudents", Name = "GetAllPassStudents")]
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


        [HttpGet("Avrg", Name = "GetAvrg")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public double GetAvrg()
        {
            return LayerBusnessLogic.Student.GetAvrg();
        }


        [HttpGet("StudentById", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentOTO> GetStudentById(int id)
        {
            if (id < 0)
            {
                return BadRequest("Bad Request");
            }
            Student student = LayerBusnessLogic.Student.Find(id);
            if (student == null)
            {
                return NotFound($"Not Found any Student with this Id = {id}");
            }
            StudentOTO s = student.SDTO();
            return Ok(s);
        }



        [HttpGet("NewStudent", Name = "AddNewStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<StudentOTO> AddNewStudent(StudentOTO student)
        {
            if (student == null || student.FullName == string.Empty || student.Grade < 0 || student.Age <  0)
            {
                return BadRequest("Bad Request ");
            }

            Student stud = new LayerBusnessLogic.Student(student);
            stud.Save();

            if (stud.Id == -1)
            {
                return NoContent();
            }
            return CreatedAtRoute("GetStudentById", new { id = student.Id }, student);

        }
    }
    

    
}
