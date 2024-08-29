using BLL.Interfaces;
using Domain.Classes;
using Domain.interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace studentManagementApi.Controllers.userController
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<User> users = _userService.GetAllUsers();
                if (users == null || users.Count == 0)
                {
                    return NotFound("No users found."); // 404 Not Found
                }
                return Ok(users); // 200 OK with the list of users
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // Fetch the user by ID
                User user = _userService.GetUserById(id);

                // Check if the user was found
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found."); // 404 Not Found
                }

                // Return the user details
                return Ok(user); // 200 OK with the user details
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null."); // 400 Bad Request
            }

            try
            {
                // Call the service to create the user
                _userService.AddUser(user);

                // Return a success response
                return Ok("User created successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("User data is null."); // 400 Bad Request
            }

            try
            {
                // Fetch the existing user by ID
                var existingUser = _userService.GetUserById(id);
                if (existingUser == null)
                {
                    return NotFound($"User with ID {id} not found."); // 404 Not Found
                }

                // Update the user information
                _userService.UpdateUser(id, updatedUser);

                // Return a success message with 200 OK
                return Ok("User updated successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }


        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                // Check if the user exists
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found."); // 404 Not Found
                }

                // Delete related records if needed (e.g., assigned roles or permissions)
                _userService.DeleteUser(id);

                // Return a success message with 200 OK
                return Ok("User deleted successfully."); // 200 OK with success message
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
                List<User> users = _userService.SearchUser(value);
                if (users == null || users.Count == 0)
                {
                    return NotFound("No users found matching the search criteria."); // 404 Not Found
                }
                return Ok(users); // 200 OK with the list of users
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


    }
}
