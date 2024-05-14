using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class Shop
    {
        private readonly ILazyLoader? lazyLoader;
        public Shop() { }
        public Shop(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;

        private User? user;
        public User? User { get => lazyLoader?.Load(this, ref user); set => user = value; }
        private ICollection<Warehouse>? products;
        public ICollection<Warehouse>? Products { get => lazyLoader?.Load(this, ref products); set => products = value; }
        private ICollection<Bill>? bills;
        public ICollection<Bill>? Bills { get => lazyLoader?.Load(this, ref bills); set => bills = value; }
    }
}
