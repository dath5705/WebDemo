﻿using Microsoft.AspNetCore.Mvc;
using WebDemo.Commands;
using WebDemo.Services;
using WebDemo.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Authorization;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalerController : Controller
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        public SalerController(
            WebDemoDatabase database,
            JwtTokenService jwtService)
        {
            this.database = database;
            this.jwtService = jwtService;
        }
        [Authorize]
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct([FromForm] Product command)
        {
            var userId = jwtService.GetId();
            var position = jwtService.GetPosition();
            if (position == 1 || position == 3)
            {
                if (database.Warehouse.Any(x => x.ProductName == command.ProductName))
                {
                    return BadRequest("Can't create this product");

                }
                else
                {
                    var product = new Warehouse()
                    {
                        ProductName = command.ProductName,
                        Quantity = command.Quantity,
                        Price = command.Price,
                        UserId = userId
                    };
                    database.Warehouse.Add(product);
                    database.SaveChanges();
                    return Ok("Create product successed");
                }
            }
            else
            {
                return BadRequest("Cannot access this section");
            }
        }
    }

}