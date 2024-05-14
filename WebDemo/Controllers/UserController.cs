using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebDemo.Commands;
using WebDemo.Models;
using WebDemo.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        private readonly ChangeInformationService changeInformationService;
        private readonly CartService cartService;
        private readonly ConvertService convertService;

        public UserController(
            WebDemoDatabase database,
            JwtTokenService jwtService,
            ChangeInformationService changeInformationService,
            CartService cartService,
            ConvertService convertService)
        {
            this.database = database;
            this.jwtService = jwtService;
            this.changeInformationService = changeInformationService;
            this.cartService = cartService;
            this.convertService = convertService;
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


        [HttpPost("ChangeNameUser")]
        public IActionResult ChangeName([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeName(command.Name);
            return Ok(result);
        }

        [HttpPost("ChangeSexUser")]
        public IActionResult ChangeSex([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeSex(command.SexId);
            return Ok(result);
        }


        [HttpPost("ChangeDateOfBirthUser")]
        public IActionResult ChangeDateOfBirth([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeDateOfBirth(command.DateTime);
            return Ok(result);
        }


        [HttpPost("ChangeNameInfor")]
        public IActionResult ChangeNameInfor([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeNameInfor(command.Name, command.Id);
            return Ok(result);
        }

        [HttpPost("ChangeNumber")]
        public IActionResult ChangeNumber([FromForm] ChangeInformation command)
        {
            string result = changeInformationService.ChangeNumber(command.Number, command.Id);
            return Ok(result);
        }
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
                var product = cartService.AddProducts(command.ProductName, command.Quantity);
                return Ok(product);
            }
            else
            {
                return BadRequest("Cannot access this section");

            }
        }
        [HttpGet("GetCartList")]
        public IActionResult GetCartList()
        {
            var userId = jwtService.GetId();
            var position = jwtService.GetPosition();
            if (position == 2)
            {
                var user = database.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null)
                {
                    return NotFound();
                }
                var Result = convertService.ConvertProduct(user);
                return Ok(Result);
            }
            else
            {
                return BadRequest("Cannot access this section");

            }
        }
        [HttpPost("CreateBill")]
        public IActionResult GetProductList([FromForm] CreateBill command) 
        {
            var userId = jwtService.GetId();
            var position = jwtService.GetPosition();
            if (position == 2)
            {
                var Result = cartService.CreateBill(command.ProductId);
                return Ok(Result);
            }
            else
            {
                return BadRequest("Cannot access this section");

            }
        }
    }
}