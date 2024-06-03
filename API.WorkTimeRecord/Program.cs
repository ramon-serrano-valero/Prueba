
using API.WorkTimeRecord.Data;
using API.WorkTimeRecord.Repositories;
using ms.communications.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger => {
    swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    { Title = "API Work Time Record", Version = "v1" });
});
builder.Services.AddScoped(typeof(IRabbitProducer), typeof(RabbitEventProducer));


//builder.Services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
builder.Services.AddTransient(typeof(IMongoContext), typeof(MongoContext));
//builder.Services.AddScoped(typeof(IMongoRepository), typeof(MongoRepository));
builder.Services.AddTransient(typeof(IMongoRepository), typeof(MongoRepository));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Work Time Record");
    });
    
}


app.UseAuthorization();

app.MapControllers();

app.Run();
