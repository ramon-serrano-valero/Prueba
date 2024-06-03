using API.Auditory.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Auditory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMongoRepository _mongorepository;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoRepository mongorepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
            _mongorepository = mongorepository;
        }
        private readonly IConfiguration _configuration;
        [HttpGet]
        public async Task CreateUserAuditoryRecord()
        {
            
            await _mongorepository.CreateMongoEntity(new Entities.MongoRecord() { UserName="rserrano", FirstName="Ramón", LastName="Serrano Valero", LastRecord= "2024-05-15T08:00:00Z", LastMode="Entrada" });
        }
        [HttpPost]
        public string PostConfig()
        {

            return _configuration.GetConnectionString("Entity:HostName");
        }

    }
}
