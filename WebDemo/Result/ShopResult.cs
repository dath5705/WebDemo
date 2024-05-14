using WebDemo.Models;

namespace WebDemo.Result
{
    public class ShopResult
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; } = 0;
        public List<ProductResult>? Products { get; set; }
    }
}
