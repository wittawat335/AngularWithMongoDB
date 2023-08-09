using Demo.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Models.Collections
{
    [BsonCollection("Category")]
    public class Category : Document
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
