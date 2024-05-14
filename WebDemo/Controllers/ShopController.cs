using Microsoft.AspNetCore.Mvc;
using WebDemo.Commands;
using WebDemo.Services;
using WebDemo.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authorization;

namespace WebDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        private readonly ChangeInformationService changeNameService;
        private readonly ShopService shopService;
        public ShopController(
            WebDemoDatabase database,
            JwtTokenService jwtService,
            ChangeInformationService changeNameService, 
            ShopService shopService)
        {
            this.database = database;
            this.jwtService = jwtService;
            this.changeNameService = changeNameService;
            this.shopService = shopService;
        }
        [HttpPost("CreateShop")]
        public IActionResult CreateShop([FromForm] CreateShop command)
        {
            var shop = shopService.CreateShop(command);
            return Ok(shop);
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromForm] Product command)
        {
            var product = shopService.CreateProduct(command);
            return Ok(product);
        }
        [HttpGet("GetShop")]
        public IActionResult GetShop()
        {
            var shop = shopService.ShowShop();
            return Ok(shop);
        }
    }
}
