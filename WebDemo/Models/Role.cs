using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class Role
    {
        private readonly ILazyLoader? lazyLoader;
        public Role() { }
        public Role(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string RoleName { get; set; } = string.Empty;

        private ICollection<User>? users;
        public ICollection<User>? Users { get => lazyLoader?.Load(this, ref users); set => users = value; }
    }
}
