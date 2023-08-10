using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Frontend.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; } = string.Empty;
        [DisplayName("Category")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [DisplayName("Status")]
        public int IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
