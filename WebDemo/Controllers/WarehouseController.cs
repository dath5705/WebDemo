using Microsoft.AspNetCore.Mvc;
using WebDemo.Commands;
using WebDemo.Services;
using WebDemo.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        public WarehouseController(
            WebDemoDatabase database,
            JwtTokenService jwtService)
        {
            this.database = database;
            this.jwtService = jwtService;
        }
        //[HttpPost("CreateProduct")]
        //public IActionResult CreateProduct([FromForm] Product command)
        //{
        //    var position = jwtService.GetPosition();
        //    if (position == "Admin")
        //    {
        //        if (database.Warehouse.Any(x => x.ProductName == command.ProductName))
        //        {
        //            return BadRequest("Can't create this product");

        //        }
        //        else
        //        {
        //            var product = new Warehouse()
        //            {
        //                ProductName = command.ProductName,
        //                Quantity = command.Quantity,
        //                Price = command.Price
        //            };
        //            database.Warehouse.Add(product);
        //            database.SaveChanges();
        //            return Ok("Create product successed");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Cannot access this section");
        //    }
        //}
    }

}
