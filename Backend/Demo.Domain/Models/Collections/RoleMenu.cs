
using Demo.Domain.Models.Base;
using MongoDB.Bson;

namespace Demo.Domain.Models.Collections
{
    [BsonCollection("RoleMenu")]
    public class RoleMenu : Document
    {
        public string Role { get; set; }

        public string MenuCode { get; set; }
    }
}
