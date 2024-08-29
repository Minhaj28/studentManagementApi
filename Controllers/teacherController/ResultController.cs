using BLL.Interfaces;
using DAL.Models;
using DAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace studentManagementApi.Controllers.teacherController
{
    [Route("api/result")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private ITeacherService _teacherService;

        public ResultController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        // GET: api/<ResultController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Fetch the result views from the service
                List<ResultView> resultViews = _teacherService.GetResultView();

                // Check if the result view list is empty
                if (resultViews == null || resultViews.Count == 0)
                {
                    return NotFound("No results found."); // 404 Not Found
                }

                // Return the result views with a 200 OK response
                return Ok(resultViews); // 200 OK with the list of result views
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during retrieval
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }




        // POST api/<ResultController>
        [HttpPost]
        public IActionResult PostExamResult([FromBody] ExamResult examResult)
        {
            if (examResult == null)
            {
                return BadRequest("ExamResult data is null."); // 400 Bad Request
            }

            try
            {
                // Call the service to add or update the exam result
                _teacherService.ExamResult(examResult);

                // Return a success response
                return Ok("Exam result processed successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }




        // DELETE api/<ResultController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteExamResult(int id)
        {
            try
            {
                // Call the service to delete the exam result by ID
                _teacherService.DeleteResult(id);

                // Return a success message with 200 OK
                return Ok("Exam result deleted successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

    }
}
