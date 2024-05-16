namespace WebDemo.Result
{
    public class BillDetailResult
    {
        public int ProductId { get; set; } = 0;
        public string? ProductName { get; set; }
        public int Quantity { get; set; } = 0;
        public int? Price { get; set; }
        public int? Total { get; set; }
    }
}
