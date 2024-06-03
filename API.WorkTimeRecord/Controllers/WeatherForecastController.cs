using API.WorkTimeRecord.Repositories;
using Microsoft.AspNetCore.Mvc;
using ms.communications.Events;
using ms.communications.Producers;

namespace API.WorkTimeRecord.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRabbitProducer _rabbitMqproducer;
        private readonly IMongoRepository _mongorepository;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoRepository mongorepository, IRabbitProducer rabbitMqproducer)
        {
            _logger = logger;
            _mongorepository = mongorepository;
            _rabbitMqproducer = rabbitMqproducer;
        }

        [HttpPost(Name = "PostCallRabbit")]
        public void PostCallRabbit()
        {
            _rabbitMqproducer.Produce(new EntityCreatedEvent() { UserName = "rserrano", Password = "12345", Role = "user" });

        }
        [HttpGet]
        public async Task CreateUserAuditoryRecord()
        {

            await _mongorepository.CreateMongoEntity(new Entities.MongoRecord() { UserName = "rserrano", FirstName = "Ramón", LastName = "Serrano Valero", LastRecord = "2024-05-15T08:00:00Z", LastMode = "Entrada" });
        }
    }
}
