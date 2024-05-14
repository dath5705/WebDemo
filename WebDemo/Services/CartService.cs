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
        public string AddProducts(string productNameInput, int productQuantityInput)
        {
            var userId = jwtService.GetId();
            var product = database.Warehouse.FirstOrDefault(x => x.ProductName.ToLower().Contains(productNameInput.ToLower()));
            if (product == null)
            {
                return "No have this product";
            }
            var items = database.ProductsInCart.Where(x => x.UserId == userId).ToList();
            if (items.Any(x => x.ProductId == product.Id))
            {
                var item = items.FirstOrDefault(x => x.ProductId == product.Id);
                item!.Quantity = productQuantityInput;
                database.ProductsInCart.Update(item);
                database.SaveChanges();
            }
            else
            {
                var products = new ProductInCart()
                {
                    UserId = userId,
                    ProductId = product.Id,
                    Quantity = productQuantityInput,
                };
                database.ProductsInCart.Add(products);
                database.SaveChanges();
            }
            if (product.Quantity < 0)
            {
                return "Out of stock";
            }
            return "Add product successed";
        }
        public string CreateBill(string productNameInput, int productIdInput, int productQuantityInput, int shoppIdInput)
        {
            var userId = jwtService.GetId();
            Bill bill = new()
            {
                UserId = userId,
                ShopId = shoppIdInput
            };
            database.Bills.Add(bill);
            database.SaveChanges();
            AddProducts(productNameInput, productQuantityInput);
            return "Create Bill Complete";
        }
        public string BillDetail(int ProductIdInput)
        {
            var userId = jwtService.GetId();
            var products = database.ProductsInCart.Where(x => x.UserId == userId)
                .Where(x => x.ProductId == ProductIdInput)
                .FirstOrDefault();
            database.ProductsInCart.Add(product!);
            database.SaveChanges();
            return "Create Bill Complete";
        }
    }
}
