using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Model;
using DataSimulation;

namespace StudentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsAPIController : ControllerBase
    {

        [HttpGet("Students", Name = "GetStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(DataStudentSamulation.Students);
        }


        [HttpGet("Pass", Name = "GetStudentsPass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Student>> GetStudentsPass()
        {
            var allSudentsPass = DataStudentSamulation.Students.Where(s => s.Grade >= 80).ToList();
            allSudentsPass.Clear(); // Clear the list to simulate the case of no students passing the exam
            if (!allSudentsPass.Any())
            {
                return NotFound("No students passed the exam");
            }
            return Ok(allSudentsPass);
        }

        [HttpGet("Student/{id}", Name = "GetStudentByID")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetStudentByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID");
            }

            var student = DataStudentSamulation.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<double> GetAverageGrade()
        {
            //DataStudentSamulation.Students.Clear(); // Clear the students list to simulate the case of no students available
            if (!DataStudentSamulation.Students.Any())
            {
                return NotFound("No students available"); // Return 0 if there are no students to avoid division by zero
            }

            var averageGrade = DataStudentSamulation.Students.Average(s => s.Grade);



            return Ok(averageGrade);
        }

        [HttpPost("AddStudent", Name = "AddStudent")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<Student> AddStudent(Student student)
        {
            if (student == null || string.IsNullOrEmpty(student.FullName) || student.Age <= 0 || student.Grade < 0 || student.Grade > 100)
            {
                return BadRequest("Invalid student data");
            }
            var newId = DataStudentSamulation.Students.Max(s => s.Id) + 1;
            student.Id = newId;
            DataStudentSamulation.Students.Add(student);
            return CreatedAtRoute("GetStudentByID", new { id = student.Id }, student);

        }
    }
}
