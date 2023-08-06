using Demo.Domain.Models.Base.Interfaces;

namespace Demo.Domain.Models.Base
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
