using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BackendNotas.Models
{
    [BsonIgnoreExtraElements]
    public class Note
    {
        public ObjectId id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime createdAt { get; set; }
    }
}
