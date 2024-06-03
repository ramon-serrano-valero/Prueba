using API.Auditory.Consumers;
using API.Auditory.Data;
using API.Auditory.Repositories;
using ms.communications.Consumers;
using ms.communications.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API - Auditory", Version = "v1" });
});
//builder.Services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
builder.Services.AddTransient(typeof(IMongoContext), typeof(MongoContext));
//builder.Services.AddScoped(typeof(IMongoRepository), typeof(MongoRepository));
builder.Services.AddTransient(typeof(IMongoRepository), typeof(MongoRepository));


builder.Services.AddSingleton(typeof(IRabbitMqConsumer), typeof(RabbitMqEntityConsumer));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API - Auditory");
    });
//}

var rabbitMqService = app.Services.GetService<IRabbitMqConsumer>();
app.UseRabbitConsumer(rabbitMqService);

app.UseAuthorization();

app.MapControllers();

app.Run();
