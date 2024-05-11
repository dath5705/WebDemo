using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class Sex
    {
        private readonly ILazyLoader? lazyLoader;
        public Sex() { }
        public Sex(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string UserSex { get; set; } = string.Empty;

        private ICollection<User>? users;
        public ICollection<User>? Users { get => lazyLoader?.Load(this, ref users); set => users = value; }
    }
}
