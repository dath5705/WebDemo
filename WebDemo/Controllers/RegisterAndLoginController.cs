
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDemo.Commands;
using WebDemo.Models;
using WebDemo.Services;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterAndLoginController : ControllerBase
    {
        private readonly ILogger<RegisterAndLoginController> _logger;
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;

        public RegisterAndLoginController(
            ILogger<RegisterAndLoginController> logger,
            WebDemoDatabase database,
            JwtTokenService jwtService)
        {
            _logger = logger;
            this.database = database;
            this.jwtService = jwtService;
        }
        [HttpPost("Register")]
        public IActionResult Register([FromForm] Register command)
        {
            var user = new User()
            {
                UserName = command.UserName,
                Password = command.Password,
                RoleId = command.RoleId,
                Name=command.Name
            };
            database.Users.Add(user);
            database.SaveChanges();
            var names = database.Users.OrderByDescending(x => x.Id);
            var name = names.First();
            name.Name = "User" + name.Id;
            database.Users.Update(name);
            database.SaveChanges();
            return Ok("Register Successed");
        }
        [HttpGet("Login")]
        public IActionResult Login([FromQuery] Register command)
        {
            var user = database.Users.Include(e=> e.Informations).FirstOrDefault(s => s.UserName == command.UserName);
            if (user == null)
            {
                return BadRequest(" No have this user.");
            }
            if (user.Password != command.Password || user.Password == null)   
            {
                return BadRequest("Wrong password.");
            }
            var key = jwtService.GenarateKey(user);
            return Ok(key);
        }
    }
}
