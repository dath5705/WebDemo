using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebDemo.Commands;
using WebDemo.Models;
using WebDemo.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        private readonly ChangeInformationService changeInformationService;
        private readonly CartService cartService;

        public UserController(
            WebDemoDatabase database,
            JwtTokenService jwtService,
            ChangeInformationService changeInformationService,
            CartService cartService)
        {
            this.database = database;
            this.jwtService = jwtService;
            this.changeInformationService = changeInformationService;
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
        [HttpPost("ChangeNameUser")]
        public IActionResult ChangeName([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeName(command.Name);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("ChangeSexUser")]
        public IActionResult ChangeSex([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeSex(command.SexId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("ChangeDateOfBirthUser")]
        public IActionResult ChangeDateOfBirth([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeDateOfBirth(command.DateTime);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("ChangeNameInfor")]
        public IActionResult ChangeNameInfor([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeNameInfor(command.Name, command.Id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("ChangeNumber")]
        public IActionResult ChangeNumber([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeNumber(command.Number, command.Id);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("ChangeAddress")]
        public IActionResult ChangeAddress([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeAddress(command.Address, command.Id);
            return Ok(result);
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