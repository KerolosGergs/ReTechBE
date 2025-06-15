//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using ReTechApi.Models;
//using ReTechApi.Data;

//namespace ReTechApi.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UsersController : ControllerBase
//    {
//        [HttpGet]
//        public ActionResult<IEnumerable<ApplicationUser>> GetUsers()
//        {
//            return Ok(UserRepository.GetAllUsers());
//        }

//        [HttpGet("{id}")]
//        public ActionResult<ApplicationUser> GetUser(string id)
//        {
//            var user = UserRepository.GetUserById(id);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            return Ok(user);
//        }

//        [HttpPost]
//        public ActionResult<ApplicationUser> CreateUser(ApplicationUser user)
//        {
//            var newUser = UserRepository.AddUser(user);
//            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
//        }

//        [HttpPut("{id}")]
//        public ActionResult<ApplicationUser> UpdateUser(string id, ApplicationUser user)
//        {
//            if (id != user.Id)
//            {
//                return BadRequest();
//            }

//            var updatedUser = UserRepository.UpdateUser(user);
//            if (updatedUser == null)
//            {
//                return NotFound();
//            }

//            return Ok(updatedUser);
//        }

//        [HttpPost("login")]
//        public ActionResult<ApplicationUser> Login([FromBody] LoginRequest request)
//        {
//            var user = UserRepository.GetUserByEmail(request.Email);
//            if (user == null || user.Password != request.Password) // Note: In production, use proper password hashing
//            {
//                return Unauthorized();
//            }

//            user.LastLogin = System.DateTime.UtcNow;
//            return Ok(user);
//        }
//    }
//}