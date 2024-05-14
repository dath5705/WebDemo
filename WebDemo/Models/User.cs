using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class User
    {
        private readonly ILazyLoader? lazyLoader;
        public User() { }
        public User(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 0;
        public string? Name { get; set; }
        public int? SexId { get; set; }
        public DateTime? DateOfBirth { get; set; }

        private Role? roll;
        public Role? Roll { get => lazyLoader?.Load(this, ref roll); set => roll = value;}

        private Sex? sex;
        public Sex? Sex { get => lazyLoader?.Load(this, ref sex); set => sex = value; }

        private ICollection<Information>? informations;
        public ICollection<Information>? Informations { get => lazyLoader?.Load(this, ref informations); set => informations = value; }

        private ICollection<ProductInCart>? productsInCart;
        public ICollection<ProductInCart>? ProductsInCart { get => lazyLoader?.Load(this, ref productsInCart); set => productsInCart = value; }
        private ICollection<Bill>? bills;
        public ICollection<Bill>? Bills { get => lazyLoader?.Load(this, ref bills); set => bills = value; }
        private Shop? shop;
        public Shop? Shop { get => lazyLoader?.Load(this, ref shop); set => shop = value; }
      
    }
}
