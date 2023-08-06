using AspNetCore.Identity.MongoDbCore.Models;
using Demo.Domain.Models.Base;
using MongoDbGenericRepository.Attributes;

namespace Demo.Domain.Models.Collections
{
    [CollectionName("Role")]
    public class Role : MongoIdentityRole<Guid>
    {
        public string RoleCode { get; set; }
        public bool IsActive { get; set; }
    }
}
