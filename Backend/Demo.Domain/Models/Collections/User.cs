using AspNetCore.Identity.MongoDbCore.Models;
using Demo.Domain.Models.Base;
using MongoDbGenericRepository.Attributes;

namespace Demo.Domain.Models.Collections
{

    [CollectionName("User")]
    public class User : MongoIdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
    }
}
