using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class ProductInCart
    {
        private readonly ILazyLoader? lazyLoader;
        public ProductInCart() { }
        public ProductInCart(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        private User? user;
        public User? User
        {
            get => lazyLoader?.Load(this, ref user); set
            {
                user = value;
            }
        }
        private Warehouse? product;
        public Warehouse? Product { get => lazyLoader?.Load(this, ref product); set { product = value; } }
    }
}
