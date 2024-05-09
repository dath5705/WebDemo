using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ChangeNameService changeNameService;

        public UserController(
            WebDemoDatabase database,
            JwtTokenService jwtService,
            ChangeNameService changeNameService)
        {
            this.database = database;
            this.jwtService = jwtService;
            this.changeNameService = changeNameService;
        }
        [Authorize]
        [HttpPost("AddInformation")]
        public IActionResult AddInformation([FromForm] AddInformation command)
        {
            var userId = jwtService.GetId();
            var information = new Informations()
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
            changeNameService.ChangeName(command.Name);
            return Ok();
        }
        //[Authorize]
        //[HttpPost("ChangeAddress")]
        //public IActionResult ChangeAddress([FromForm] AddInformation command)
        //{
        //    changeNameService.ChangeAddress(command.Address);
        //    return Ok();
        //}
        //[HttpPost("CreateBill")]
        //public IActionResult CreateBill([FromForm] CreateBill command)
        //{
        //    var position = jwtService.GetPosition();
        //    if (position == "Sales Staff")
        //    {
        //        createBills.CreateBill(command.Customer, command.Address);
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest("Cannot access this section");
        //    }
        //}

        //[HttpPost("AddProducts")]
        //public IActionResult AddProduct([FromQuery] AddProduct command)
        //{
        //    var position = jwtService.GetPosition();
        //    if (position == "Sales Staff")
        //    {
        //        createBills.AddProduct(command.Product, command.Quantity);
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest("Cannot access this section");

        //    }
        //}
    }
}