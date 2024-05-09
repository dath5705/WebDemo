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
        public string Name { get; set; } = string.Empty;

        private Roles? roll;
        public Roles? Roll { get => lazyLoader?.Load(this, ref roll); set => roll = value;}

        private ICollection<Warehouse>? products;
        public ICollection<Warehouse>? Products { get => lazyLoader?.Load(this, ref products); set => products = value; }

        private ICollection<Informations>? informations;
        public ICollection<Informations>? Informations { get => lazyLoader?.Load(this, ref informations); set => informations = value; }
    }
}
