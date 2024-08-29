using Microsoft.AspNetCore.Mvc;
using Domain.Classes;
using BLL.Interfaces;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace studentManagementApi.Controllers.studentController
{
    [Route("api/student")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class StudentController : ControllerBase
    {

        private IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        // GET: api/<StudentController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Student> students = _studentService.GetAllStudents();
                if (students == null || students.Count == 0)
                {
                    return NotFound("No students found."); // 404 Not Found
                }
                return Ok(students); // 200 OK with the list of students
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Student student = _studentService.GetStudentById(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found."); // 404 Not Found
                }
                return Ok(student); // 200 OK with the student data
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student is null.");
            }

            try
            {
                _studentService.CreateStudent(student);
                return Ok("Student created successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest("Student data is null."); // 400 Bad Request
            }

            try
            {
                var existingStudent = _studentService.GetStudentById(id);
                if (existingStudent == null)
                {
                    return NotFound($"Student with ID {id} not found."); // 404 Not Found
                }

                // Update the student information
                _studentService.UpdateStudent(id,updatedStudent);

                // Return a success message with 200 OK
                return Ok("Student updated successfully."); // 200 OK with success message
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Check if the student exists
                var student = _studentService.GetStudentById(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found."); // 404 Not Found
                }

                // Delete the student
                _studentService.DeleteStudentEnrollmentByStudentId(id);
                _studentService.DeleteExamResultByStudentId(id);
                _studentService.DeleteStudent(id);

                // Return a success message with 200 OK
                return Ok("Student deleted successfully."); // 200 OK with success message
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Search value cannot be null or empty."); // 400 Bad Request
            }

            try
            {
                List<Student> students =  _studentService.SearchStudents(value);
                if (students == null || students.Count == 0)
                {
                    return NotFound("No students found matching the search criteria."); // 404 Not Found
                }
                return Ok(students); // 200 OK with the list of students
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


    }
}
