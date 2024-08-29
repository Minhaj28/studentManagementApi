using BLL.Interfaces;
using BLL.Services;
using Domain.Classes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace studentManagementApi.Controllers.courseController
{
   // private CourseService courseService;
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: api/<CourseController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Course> courses = _courseService.GetAllCourses();
                if (courses == null || courses.Count == 0)
                {
                    return NotFound("No courses found."); // 404 Not Found
                }
                return Ok(courses); // 200 OK with the list of courses
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // Fetch the course by ID
                Course course = _courseService.GetCourseById(id);

                // Check if the course was found
                if (course == null)
                {
                    return NotFound($"Course with ID {id} not found."); // 404 Not Found
                }

                // Return the course details
                return Ok(course); // 200 OK with the course details
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // POST api/<CourseController>
        [HttpPost]
        public IActionResult Post([FromBody] Course course)
        {
            if (course == null)
            {
                return BadRequest("Course is null."); // 400 Bad Request
            }

            try
            {
                // Call the service to create the course
                _courseService.CreateCourse(course);

                // Return a success response
                return Ok("Course created successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course updatedCourse)
        {
            if (updatedCourse == null)
            {
                return BadRequest("Course data is null."); // 400 Bad Request
            }

            try
            {
                // Fetch the existing course by ID
                var existingCourse = _courseService.GetCourseById(id);
                if (existingCourse == null)
                {
                    return NotFound($"Course with ID {id} not found."); // 404 Not Found
                }

                // Update the course information
                _courseService.UpdateCourse(id, updatedCourse);

                // Return a success message with 200 OK
                return Ok("Course updated successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Check if the course exists
                var course = _courseService.GetCourseById(id);
                if (course == null)
                {
                    return NotFound($"Course with ID {id} not found."); // 404 Not Found
                }

                // Delete related records if needed (e.g., enrolled students or associated data)
                _courseService.DeleteStudentEnrollmentByCourseId(id);
                _courseService.DeleteCourse(id);

                // Return a success message with 200 OK
                return Ok("Course deleted successfully."); // 200 OK with success message
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
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
                List<Course> courses = _courseService.SearchCourses(value);
                if (courses == null || courses.Count == 0)
                {
                    return NotFound("No courses found matching the search criteria."); // 404 Not Found
                }
                return Ok(courses); // 200 OK with the list of courses
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


    }
}
