using WebDemo.Models;

namespace WebDemo.Services
{
    public class CartService
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        public CartService(WebDemoDatabase database, JwtTokenService jwtService)
        {
            this.database = database;
            this.jwtService = jwtService;
        }
        public string AddProducts(string ProductNameInput, int ProductQuantityInput)
        {
            var userId = jwtService.GetId();
            var product1 = database.Warehouse.FirstOrDefault(x => x.ProductName.ToLower().Contains(ProductNameInput.ToLower()));
            if (product1 == null)
            {
                return "No have this product";
            }
            var items = database.ProductsInCart.Where(x=> x.UserId==userId).ToList();
            if (items.Any(x => x.ProductId == product1.Id))
            {
                var item = items.FirstOrDefault(x => x.ProductId == product1.Id);
                item!.Quantity = ProductQuantityInput;
                database.ProductsInCart.Update(item);
                database.SaveChanges();
            }
            else
            {
                var products = new ProductInCart()
                {
                    UserId = userId,
                    ProductId = product1.Id,
                    Quantity = ProductQuantityInput,
                };
                database.ProductsInCart.Add(products);
                database.SaveChanges();
            }
            if (product1.Quantity < 0)
            {
                return "Out of stock";
            }
            return "Add product successed";
        }
    }
}
