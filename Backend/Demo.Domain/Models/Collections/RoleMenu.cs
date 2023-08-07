
using Demo.Domain.Models.Base;

namespace Demo.Domain.Models.Collections
{
    [BsonCollection("RoleMenu")]
    public class RoleMenu : Document
    {
        public string MenuCode { get; set; }

        public string RoleCode { get; set; }
    }
}
