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
    }
}
