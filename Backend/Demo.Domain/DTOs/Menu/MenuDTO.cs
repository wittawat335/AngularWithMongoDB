using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.DTOs.Menu
{
    public class MenuDTO
    {
        public string Id { get; set; }
        public string MenuCode { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int IsActive { get; set; }
    }
}
