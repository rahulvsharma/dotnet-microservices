using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CatalogDatabaseSettings>(builder.Configuration.GetSection("CatalogDatabase"));
// builder.Services.AddSingleton<IRepository<Item>, MongoRepository<Item>>();

builder.Services.AddControllers(options => {
    options.SuppressAsyncSuffixInActionNames = false;
});
// BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
// BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

builder.Services.AddMongo().AddMongoRepository<Item>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
