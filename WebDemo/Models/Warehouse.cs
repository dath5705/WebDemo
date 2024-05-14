using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class Warehouse
    {
        private readonly ILazyLoader? lazyLoader;
        public Warehouse() { }
        public Warehouse(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public int Price { get; set; } = 0;
        public int ShopId { get; set; } = 0;

        private Shop? shop;
        public Shop? Shop { get => lazyLoader?.Load(this, ref shop); set => shop = value; }

        private ICollection<ProductInCart>? productsInCart;
        public ICollection<ProductInCart>? ProductsInCart { get => lazyLoader?.Load(this, ref productsInCart); set => productsInCart = value; }
        private ICollection<BillDetail>? billsDetail;
        public ICollection<BillDetail>? BillsDetail { get => lazyLoader?.Load(this, ref billsDetail); set => billsDetail = value; }
    }
}
