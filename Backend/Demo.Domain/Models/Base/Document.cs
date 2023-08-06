using Demo.Domain.Models.Base.Interfaces;
using MongoDB.Bson;

namespace Demo.Domain.Models.Base
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }
}
