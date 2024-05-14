using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class BillDetail
    {
        private readonly ILazyLoader? lazyLoader;
        public BillDetail() { }
        public BillDetail(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int BillId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int Quantity { get; set; } = 0;

        private Bill? bill;
        public Bill? Bill { get => lazyLoader?.Load(this, ref bill); set => bill = value; }

        private Warehouse? product;
        public Warehouse? Product { get => lazyLoader?.Load(this, ref product); set => product = value; }
    }
}
