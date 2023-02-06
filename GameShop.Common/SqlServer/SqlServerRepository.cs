// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;

// namespace SqlServerRepository
// {
//     public static class Extensions
//     {
//         public static IServiceCollection AddMongo(this IServiceCollection services)
//         {
//             services.AddSingleton(serviceProvider =>
//             {
//                 var configuration = serviceProvider.GetService<IConfiguration>();
//                 var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
//                 var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
//                 var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
//                 return mongoClient.GetDatabase(serviceSettings.ServiceName);
//             });
//             return services;
//         }

//         public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : IEntity
//         {
//             services.AddSingleton<IRepository<T>>(serviceProvider =>
//             {
//                 var database = serviceProvider.GetService<IMongoDatabase>();
//                 return new MongoRepository<T>(database, collectionName); //games
//             });
//             return services;
//         }
//     }
// //     builder.Services.AddDbContext<SchoolContext>(options =>
// //   options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));
// }