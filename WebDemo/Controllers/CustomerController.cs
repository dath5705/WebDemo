using Microsoft.AspNetCore.Mvc;
using WebDemo.Services;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;

        public CustomerController(
            WebDemoDatabase database,
            JwtTokenService jwtService)
        {
            this.database = database;
            this.jwtService = jwtService;
        }
        [HttpPost("CreateBill")]
        public IActionResult CreateBill([FromForm] CreateBill command)
        {
            var position = jwtService.GetPosition();
            if (position == "Sales Staff")
            {
                createBills.CreateBill(command.Customer, command.Address);
                return Ok();
            }
            else
            {
                return BadRequest("Cannot access this section");
            }
        }

        [HttpPost("AddProducts")]
        public IActionResult AddProduct([FromQuery] AddProduct command)
        {
            var position = jwtService.GetPosition();
            if (position == "Sales Staff")
            {
                createBills.AddProduct(command.Product, command.Quantity);
                return Ok();
            }
            else
            {
                return BadRequest("Cannot access this section");

            }
        }
    }
}