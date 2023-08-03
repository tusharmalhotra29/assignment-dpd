namespace Project1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Project1.Models;
    using Project1.Models.DTO;
    using System;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>(); // For simplicity, use an in-memory list to store users.

        [HttpPost("register")]
        public IActionResult Register(UserRegistrationRequest request)
        {
            // Validation - For simplicity, you can use basic validation here.
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Username, email, and password are required." });
            }

            // Check if the username or email is already taken (you can modify this as per your requirement).
            if (users.Any(u => u.Username == request.Username) || users.Any(u => u.Email == request.Email))
            {
                return Conflict(new { message = "Username or email already taken." });
            }

            
            var newUser = new User
            {
                Id = users.Count + 1,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password, // In a real-world scenario, you should hash the password before storing it.
                FullName = request.FullName,
                Age = request.Age,
                Gender = request.Gender
            };
            users.Add(newUser);

            
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }

}
