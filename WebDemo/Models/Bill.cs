using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace WebDemo.Models
{
    public class Bill
    {
        private readonly ILazyLoader? lazyLoader;
        public Bill() { }
        public Bill(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int Id { get; set; } = 0;
        public string BillNumber
        {
            get
            {
                return "Bill " + Id;
            }
            set
            {
            }
        }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; } = 0;
        public string Status { get; set; } = "Ordered";
        public int ShopId { get; set; } = 0;
        private User? user;
        public User? User { get => lazyLoader?.Load(this, ref user); set => user = value; }
        private Shop? shop;
        public Shop? Shop { get => lazyLoader?.Load(this, ref shop); set => shop = value; }
        private ICollection<BillDetail>? billsDetail;
        public ICollection<BillDetail>? BillsDetail { get => lazyLoader?.Load(this, ref billsDetail); set => billsDetail = value; }
    }
}

