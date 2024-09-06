using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Settings;

namespace Play.Catalog.Service.Repositories
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        
            // services. serviceProvider => {
                // var configuration = serviceProvider.GetService<IConfiguration>();
                // return services.Configure<CatalogDatabaseSettings>(configuration.GetSection("CatalogDatabase"));
            // });

            return services;

        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services) where T : IEntity
        {
            services.AddSingleton<IRepository<Item>, MongoRepository<Item>>();
            return services;
        }
    }
    
}