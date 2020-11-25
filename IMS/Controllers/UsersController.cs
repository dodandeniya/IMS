using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMS.Models;
using IMS.data;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using IMS.Constants;
using IMS.interfaces;

namespace IMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService service)
        {
            userService = service;
        }

        // GET: api/Users
        [Authorize(Roles = Roles.AdminOrManager)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await userService.GetAllUsers();
        }

        [Authorize(Roles = Roles.AdminOrManager)]
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<dynamic>> GetUser([FromBody] Authenticate model)
        {
            var user = await userService.Authenticate(model.Email, model.Password);

            if (user == null)
            {
                return NotFound(new { Error = "Email or Password incorrect" });
            }

            if (!user.IsEnabled)
            {
                return Unauthorized(new { Error = "User account is disabled" });
            }

            return new { user.FirstName, user.LastName, user.Role, user.IsEnabled, user.Token, user.Id };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await userService.UpdateUser(id, user);
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                await userService.CreateUser(user);
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            return await userService.DeleteUser(id);
        }
    }
}
