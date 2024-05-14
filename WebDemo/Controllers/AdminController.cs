using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WebDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebDemo.Commands;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        private readonly ChangeInformationService changeInformationService;
        private readonly CartService cartService;
        private readonly ConvertService convertService;

        public AdminController(
            WebDemoDatabase database,
            JwtTokenService jwtService,
            ChangeInformationService changeInformationService,
            CartService cartService, ConvertService convertService)
        {
            this.database = database;
            this.jwtService = jwtService;
            this.changeInformationService = changeInformationService;
            this.cartService = cartService;
            this.convertService = convertService;
        }
        [Authorize]
        [HttpGet("GetUserList")]
        public IActionResult GetUser([FromQuery] PageGeneration command)
        {
            var userId = jwtService.GetId();
            var position = jwtService.GetPosition();
            if (position == 1)
            {
                var listusers = database.Users.Include(x=>x.ProductsInCart).Include(x => x.Informations).Skip(command.PageIndex * command.PageCount).Take(command.PageCount).ToList();
                var result = convertService.ConvertUser(listusers);
                return Ok(result);
            }
            else
            {
                return BadRequest("Cannot access this section");
            }
        }
    }
}
