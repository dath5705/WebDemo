using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using WebDemo.Models;
using WebDemo.Commands;
using WebDemo.Result;

namespace WebDemo.Services
{
    public class ShopService
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        public ShopService(WebDemoDatabase database, JwtTokenService jwtService)
        {
            this.database = database;
            this.jwtService = jwtService;
        }
        public string CreateProduct(Product command)
        {
            var userId = jwtService.GetId();
            var shop = database.Shops.FirstOrDefault(x => x.UserId == userId);
            if (shop == null)
            {
                return "Cannot access this section";
            }
            else
            {
                if (database.Warehouse.Any(x => x.ProductName == command.ProductName))
                {
                    return "Can't create this product";
                }
                else
                {
                    if (shop == null)
                    {
                        return "Can't add product";
                    }
                    var product = new Warehouse()
                    {
                        ProductName = command.ProductName,
                        Quantity = command.Quantity,
                        Price = command.Price,
                        ShopId = shop.Id,
                    };
                    database.Warehouse.Add(product);
                    database.SaveChanges();
                    return "Create product successed";
                }
            }
        }
        public string CreateShop(CreateShop command)
        {
            var userId = jwtService.GetId();
            if (database.Shops.Any(x => x.UserId == userId))
            {
                return "You had a shop";
            };
            var shop = new Shop()
            {
                Name = command.Name,
                UserId = userId,
            };
            database.Shops.Add(shop);
            database.SaveChanges();
            return "Create shop successed";
        }
        public ShopResult ShowShop()
        {
            var userId = jwtService.GetId();
            var shop = database.Shops.FirstOrDefault(x => x.UserId == userId);
            if (shop != null)
            {
                ShopResult result = new()
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    UserId = userId,
                    Products = shop.Products?.Select(e =>
                    {
                        ProductResult product = new()
                        {
                            ProductId = e.Id,
                            ProductName = e.ProductName,
                            Quantity = e.Quantity,
                            Price = e.Price,
                        };
                        return product;
                    }).ToList()
                };
                return result;
            }
            return new();
        }
    }
}