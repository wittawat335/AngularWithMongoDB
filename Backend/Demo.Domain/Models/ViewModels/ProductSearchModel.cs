using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Models.ViewModels
{
    public class ProductSearchModel
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string IsActive { get; set; }
    }
}
