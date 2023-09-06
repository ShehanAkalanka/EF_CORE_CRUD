using EF_CORE_CRUD.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_CORE_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            var user = await _context.Users.Where(c => c.Id == userId).ToListAsync();
            if(user!=null)
            return Ok(user);
            else
            return BadRequest("No Data Found");

        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User user)
        {
            var _user = await _context.Users.FindAsync(user.Id);

            if (_user == null)
                return BadRequest("No User Found");
            _user.Name = user.Name;
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return BadRequest("No User Found");

             _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

    }
}
