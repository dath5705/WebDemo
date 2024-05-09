using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class Informations
    {
        private readonly ILazyLoader? lazyLoader;
        public Informations() { }
        public Informations(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; }= string.Empty;
        public string Address { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;

        private User? user;
        public User? User { get => lazyLoader?.Load(this, ref user); set => user = value; }
    }
}
