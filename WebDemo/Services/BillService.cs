using WebDemo.Commands;
using WebDemo.Models;
using WebDemo.Result;

namespace WebDemo.Services
{
    public class BillService
    {
        private readonly WebDemoDatabase database;
        private readonly JwtTokenService jwtService;
        private readonly CartService cartService;
        public BillService(WebDemoDatabase database, JwtTokenService jwtService, CartService cartService)
        {
            this.database = database;
            this.jwtService = jwtService;
            this.cartService = cartService;
        }
        public string CreateBill(string productNameInput, int productQuantityInput, int informationIdInput)
        {
            var userId = jwtService.GetId();
            var product = database.Warehouse.FirstOrDefault(x => x.ProductName.ToLower().Contains(productNameInput.ToLower()));
            if (product == null)
            {
                return "Can't order ";
            }
            var information = database.Informations.Where(x => x.UserId == userId).Where(x => x.Id == informationIdInput).FirstOrDefault();
            Bill bill = new()
            {
                UserId = userId,
                ShopId = product.ShopId,
                InformationId = informationIdInput,
            };
            database.Bills.Add(bill);
            database.SaveChanges();
            cartService.AddProducts(productNameInput, productQuantityInput);
            return "Create Bill Complete";
        }
        public string BillDetail(int productIdInput, string productNameInput)
        {
            var userId = jwtService.GetId();
            var productId = database.Warehouse.FirstOrDefault(x => x.ProductName.ToLower().Contains(productNameInput.ToLower()));
            var product = database.ProductsInCart.Where(x => x.UserId == userId)
                .Where(x => x.ProductId == (productId == null ? productIdInput : productId.Id))
                .FirstOrDefault();
            var bill = database.Bills.Where(x => x.UserId == userId).Where(x => x.Status == "Ordered").FirstOrDefault();
            if (product == null)
            {
                return "You don't choose product";
            }
            BillDetail billDetail = new()
            {
                BillId = bill!.Id,
                ProductId = product.ProductId,
                Quantity = product.Quantity,
            };
            database.BillsDetail.Add(billDetail);
            database.ProductsInCart.Remove(product);
            productId!.Quantity -= product.Quantity;
            database.Warehouse.Update(productId);
            database.SaveChanges();
            return "Create Bill Complete";
        }
        public List<BillResult> GetBill()
        {
            var userId = jwtService.GetId();
            var bills = database.Bills.Where(x => x.UserId == userId).Where(x => x.Status == "Ordered").ToList();
            List<BillResult> billResults = bills.Select(x =>
            {
                var details = x.BillsDetail?.Select(e =>
                {
                    BillDetailResult detail = new()
                    {
                        ProductId = e.ProductId,
                        ProductName = e.Product?.ProductName,
                        Quantity = e.Quantity,
                        Price = e.Product?.Price,
                        Total = e.Quantity * e.Product?.Price,
                    };
                    return detail;
                }).ToList();
                var infor = database.Informations.FirstOrDefault(e => e.Id == x.InformationId);
                if (infor == null)
                {
                    infor = new();
                }
                BillResult bill = new()
                {
                    Id = x.Id,
                    BillNumber = x.BillNumber,
                    Information = new()
                    {
                        Id = infor.Id,
                        Name = infor.Name,
                        Number = infor.Number,
                        Address = infor.Address,
                    },
                    Date = x.Date,
                    Status = x.Status,
                    ShopId = x.ShopId,
                    Products = details,
                    Total = details?.Sum(e => e.Total),
                };
                return bill;
            }).ToList();
            return billResults;
        }
    }
}
