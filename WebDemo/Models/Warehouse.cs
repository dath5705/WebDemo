using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class Warehouse
    {
        private readonly ILazyLoader? lazyLoader;
        public Warehouse() { }
        public Warehouse(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public int Price { get; set; } = 0;
        public int Id { get; set; } = 0;
        //private ICollection<BillDetail>? billsDetail;
        //public ICollection<BillDetail>? BillsDetail { get => lazyLoader?.Load(this, ref billsDetail); set => billsDetail = value; }
    }
}
