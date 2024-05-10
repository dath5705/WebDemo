using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebDemo.Commands;
using WebDemo.Models;
using WebDemo.Services;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        private readonly ChangeInformationService changeNameService;
        private readonly CartService cartService;

        public UserController(
            WebDemoDatabase database,
            JwtTokenService jwtService,
            ChangeInformationService changeNameService,
            CartService cartService)
        {
            this.database = database;
            this.jwtService = jwtService;
            this.changeNameService = changeNameService;
            this.cartService = cartService;
        }
        [Authorize]
        [HttpPost("AddInformation")]
        public IActionResult AddInformation([FromForm] AddInformation command)
        {
            var userId = jwtService.GetId();
            var information = new Information()
            {
                Name = command.Name,
                Number = command.Number,
                Address = command.Address,
                UserId = userId,
            };
            database.Informations.Add(information);
            database.SaveChanges();
            return Ok(information);
        }

        [Authorize]
        [HttpPost("ChangeName")]
        public IActionResult ChangeName([FromForm] AddInformation command)
        {
            string result = changeNameService.ChangeName(command.Name);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("ChangeAddress")]
        public IActionResult ChangeAddress([FromForm] ChangeInformation command)
        {
            var userId =jwtService.GetId();
            var inforList = database.Informations.Where(x => x.UserId == userId).ToList();
            if (inforList == null)
            {
                return BadRequest("You dont have address. please add your address");
            }
            var infor = inforList.FirstOrDefault(e=> e.Id == command.Id);
            if (infor == null)
            {
                return Ok("No have this user");
            }
            infor.Address = command.Address;
            database.Informations.Update(infor);
            database.SaveChanges();
            return Ok("Change Complete");
        }

        [HttpPost("AddProductsToCart")]
        public IActionResult AddProduct([FromForm] AddProductsToCart command)
        {
            var position = jwtService.GetPosition();
            if (position == 2)
            {
                 var a =cartService.AddProducts(command.ProductName, command.Quantity);
                return Ok(a);
            }
            else
            {
                return BadRequest("Cannot access this section");

            }
        }
    }
}