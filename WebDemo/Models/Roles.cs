using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class Roles
    {
        private readonly ILazyLoader? lazyLoader;
        public Roles() { }
        public Roles(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string RoleName { get; set; } = string.Empty;

        private ICollection<User>? users;
        public ICollection<User>? Users { get => lazyLoader?.Load(this, ref users); set => users = value; }
    }
}
