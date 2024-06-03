using API.Auditory.Data;
using API.Auditory.Entities;

namespace API.Auditory.Repositories
{
    public interface IMongoRepository
    {
        Task<bool> CreateMongoEntity(MongoRecord mongoRecord);
    }
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoContext _context;

        //private readonly Tracer _tracer;
        public MongoRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMongoEntity(MongoRecord mongoRecord)
        {

            var myMongoEntity = new MyMongoEntity() { userName = mongoRecord.UserName, firstName = mongoRecord.FirstName, lastName = mongoRecord.LastName, lastRecord = mongoRecord.LastRecord, mode = mongoRecord.LastMode };
            await _context.MongoCollection.InsertOneAsync(myMongoEntity);
           
            return true;
        }
    }
}
