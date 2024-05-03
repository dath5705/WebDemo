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
        //private ICollection<Customer>? customers;
        //public ICollection<Customer>? Customers { get => lazyLoader?.Load(this, ref customers); set => customers = value; }
        //private ICollection<Bill>? bills;
        //public ICollection<Bill>? Bills { get => lazyLoader?.Load(this, ref bills); set => bills = value; }
    }
}
