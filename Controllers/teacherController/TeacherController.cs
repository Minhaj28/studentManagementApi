using BLL.Interfaces;
using DAL.Models;
using DAL.ViewModels;
using Domain.Classes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace studentManagementApi.Controllers.teacherController
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {

        private ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        // GET: api/<TeacherController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Teacher> teachers = _teacherService.GetAllTeachers();
                if (teachers == null || teachers.Count == 0)
                {
                    return NotFound("No teachers found."); // 404 Not Found
                }
                return Ok(teachers); // 200 OK with the list of teachers
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // GET api/<TeacherController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // Fetch the teacher by ID
                Teacher teacher = _teacherService.GetTeacherById(id);

                // Check if the teacher was found
                if (teacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found."); // 404 Not Found
                }

                // Return the teacher details
                return Ok(teacher); // 200 OK with the teacher details
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // POST api/<TeacherController>
        [HttpPost]
        public IActionResult Post([FromBody] Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher is null."); // 400 Bad Request
            }

            try
            {
                // Call the service to create the teacher
                _teacherService.CreateTeacher(teacher);

                // Return a success response
                return Ok("Teacher created successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // PUT api/<TeacherController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Teacher updatedTeacher)
        {
            if (updatedTeacher == null)
            {
                return BadRequest("Teacher data is null."); // 400 Bad Request
            }

            try
            {
                var existingTeacher = _teacherService.GetTeacherById(id);
                if (existingTeacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found."); // 404 Not Found
                }

                // Update the teacher information
                _teacherService.UpdateTeacher(id, updatedTeacher);

                // Return a success message with 200 OK
                return Ok("Teacher updated successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // DELETE api/<TeacherController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Check if the teacher exists
                var teacher = _teacherService.GetTeacherById(id);
                if (teacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found."); // 404 Not Found
                }

                // Delete the teacher
                _teacherService.DeleteAssignedTeachersByTeacherId(id);
                _teacherService.DeleteTeacher(id);

                // Return a success message with 200 OK
                return Ok("Teacher deleted successfully."); // 200 OK with success message
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
                List<Teacher> teachers = _teacherService.SearchTeachers(value);
                if (teachers == null || teachers.Count == 0)
                {
                    return NotFound("No teachers found matching the search criteria."); // 404 Not Found
                }
                return Ok(teachers); // 200 OK with the list of teachers
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        //AssignedTeacher

        [HttpGet("assigned-teachers")]
        public IActionResult GetAssignedTeachers()
        {
            try
            {
                // Retrieve the list of assigned teacher views
                List<AssignedTeacherView> assignedTeachers = _teacherService.GetAssignedTeacherView();

                // Check if the list is empty or null
                if (assignedTeachers == null || assignedTeachers.Count == 0)
                {
                    return NotFound("No assigned teachers found."); // 404 Not Found
                }

                // Return the list of assigned teacher views with a 200 OK status
                return Ok(assignedTeachers); // 200 OK
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a 500 Internal Server Error status
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

        [HttpPost("assign-teacher")]
        public IActionResult AssignTeacher([FromBody] AssignedTeacher assignedTeacher)
        {
            if (assignedTeacher == null)
            {
                return BadRequest("AssignedTeacher data is null."); // 400 Bad Request
            }

            try
            {
                // Call the service method to process the assigned teacher
                _teacherService.AssignedTeacher(assignedTeacher);

                // Return a success response with a 200 OK status
                return Ok("Teacher assigned successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a 500 Internal Server Error status
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

        [HttpDelete("assigned-teacher/{id}")]
        public IActionResult DeleteAssignedTeacher(int id)
        {
            try
            {
                // Delete the assigned teacher record
                _teacherService.DeleteAssignedTeacher(id);

                // Return a success message with 200 OK
                return Ok("Assigned teacher deleted successfully."); // 200 OK with success message
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }





    }
}
