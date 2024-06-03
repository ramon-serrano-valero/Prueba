using MongoDB.Bson.Serialization.Attributes;

namespace API.Auditory.Entities
{
    public class MyMongoEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string lastRecord { get; set; }
        public string mode { get; set; }
    }
}
