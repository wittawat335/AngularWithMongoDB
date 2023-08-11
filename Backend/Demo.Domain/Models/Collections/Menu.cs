using Demo.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Models.Collections
{
    [BsonCollection("Menu")]
    public class Menu : Document
    {
        public string MenuCode { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Level { get; set; }
        public string ParentCode { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
