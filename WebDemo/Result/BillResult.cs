namespace WebDemo.Result
{
    public class BillResult
    {
        public int Id { get; set; } = 0;
        public string BillNumber { get; set; } = string.Empty;
        public InformationResult? Information { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Status { get; set; } = string.Empty;
        public int ShopId { get; set; } = 0;
        public List<BillDetailResult>? Products { get; set; }
        public int? Total {  get; set; }
       
    }
}
