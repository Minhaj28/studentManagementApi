using BLL.Interfaces;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace studentManagementApi.Controllers.teacherController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private ITeacherService _teacherService;

        public ExamController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        // GET: api/<ExamController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Fetch the exam views from the service
                List<ExamView> examViews = _teacherService.GetExamView();

                // Check if the exam view list is empty
                if (examViews == null || examViews.Count == 0)
                {
                    return NotFound("No exams found."); // 404 Not Found
                }

                // Return the exam views with a 200 OK response
                return Ok(examViews); // 200 OK with the list of exam views
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during retrieval
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }



        // POST api/<ExamController>
        [HttpPost]
        public IActionResult TakeExam([FromBody] Exam exam)
        {
            if (exam == null)
            {
                return BadRequest("Exam data is null."); // 400 Bad Request
            }

            try
            {
                // Call the service to process the exam
                _teacherService.TakeExam(exam);

                // Return a success response
                return Ok("Exam create successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // DELETE api/<ExamController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteExam(int id)
        {
            try
            {
                // Call the service to delete the exam
                _teacherService.DeleteExam(id);

                // Return a success message with 200 OK
                return Ok("Exam deleted successfully."); // 200 OK with success message
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

    }
}
