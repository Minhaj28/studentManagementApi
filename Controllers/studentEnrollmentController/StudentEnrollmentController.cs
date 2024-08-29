using BLL.Interfaces;
using DAL.ViewModels;
using Domain.Classes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace studentManagementApi.Controllers.studentEnrollmentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEnrollmentController : ControllerBase
    {
        private IStudentEnrollmentService _studentEnrollmentService;
        public StudentEnrollmentController(IStudentEnrollmentService studentEnrollmentService)
        {
            _studentEnrollmentService = studentEnrollmentService;
        }
        // GET: api/<StudentEnrollmentController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Assuming _studentEnrollmentService is injected and provides access to student enrollment data
                List<StudentEnrollment> enrollments = _studentEnrollmentService.GetStudentEnrollment();

                if (enrollments == null || enrollments.Count == 0)
                {
                    return NotFound("No student enrollments found."); // 404 Not Found
                }

                return Ok(enrollments); // 200 OK with the list of enrollments
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // GET api/<StudentEnrollmentController>/5
       /* [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<StudentEnrollmentController>
        [HttpPost]
        public IActionResult Post([FromBody] StudentEnrollment enrollment)
        {
            if (enrollment == null)
            {
                return BadRequest("Student enrollment data is null."); // 400 Bad Request
            }

            try
            {
                // Call the service to create the student enrollment
                _studentEnrollmentService.EnrollStudent(enrollment);

                // Return a success response
                return Ok("Student enrollment created successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // PUT api/<StudentEnrollmentController>/5
        /* [HttpPut("{id}")]
         public void Put(int id, [FromBody] string value)
         {
         }*/

        // DELETE api/<StudentEnrollmentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Delete the student enrollment
                _studentEnrollmentService.DeleteEnrollment(id);

                // Return a success message with 200 OK
                return Ok("Student Enrollment deleted successfully."); // 200 OK with success message
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        [HttpGet("allStudentEnrollmentView")]
        public IActionResult AllStudentEnrollmentView()
        {
            try
            {
                // Retrieve all student enrollment views
                List<StudentEnrollmentView> enrollments = _studentEnrollmentService.GetStudentEnrollmentView();

                // Check if the list is empty
                if (enrollments == null || enrollments.Count == 0)
                {
                    return NotFound("No student enrollments found."); // 404 Not Found
                }

                // Return the list of enrollment views
                return Ok(enrollments); // 200 OK with the list of enrollments
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }

    }
}
