using API.Auditory.Entities;
using MongoDB.Driver;
namespace API.Auditory.Data
{
    public interface IMongoContext
    {
        IMongoCollection<MyMongoEntity> MongoCollection { get; }
    }
    public class MongoContext: IMongoContext
    {
        private readonly IConfiguration _configuration;
        private IMongoDatabase _mongoDatabase;


        public IMongoCollection<MyMongoEntity> MongoCollection => _mongoDatabase.GetCollection<MyMongoEntity>(_configuration.GetConnectionString("Entity:Collection"));

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoDatabase = new MongoClient(String.Concat(
                                                "mongodb://", configuration.GetConnectionString("Entity:HostName"),
                                                ":", configuration.GetConnectionString("Entity:Port"))
                                            ).GetDatabase(configuration.GetConnectionString("Entity:DataBase"));
        }
    }
}
