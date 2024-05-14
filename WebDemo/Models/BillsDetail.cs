using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebDemo.Models
{
    public class BillsDetail
    {
        private readonly ILazyLoader? lazyLoader;
        public BillsDetail() { }
        public BillsDetail(ILazyLoader loader)
        {
            lazyLoader = loader;
        }
        public int BillId { get; set; } = 0;
        public string ProductId { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;

        private Bill? bill;
        public Bill? Bill { get => lazyLoader?.Load(this, ref bill); set => bill = value; }

        private Warehouse? product;
        public Warehouse? Product { get => lazyLoader?.Load(this, ref product); set => product = value; }
    }
}
