using Frontend.DTOs;

namespace Frontend.Models.ViewModel.Product
{
    public class ProductViewModel
    {
        public ProductDTO productDTO { get; set; }
        public List<ProductDTO> listProduct { get; set; }
        public List<CategoryDTO> listCategory { get; set; }
        public string action { get; set; }
        public ProductViewModel()
        {
            productDTO = new ProductDTO();
            listCategory = new List<CategoryDTO>();
            listProduct = new List<ProductDTO>();
        }
    }
}
