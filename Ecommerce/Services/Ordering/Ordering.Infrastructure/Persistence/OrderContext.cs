using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext 
    {

        public IMongoDatabase Database { get; }

        private readonly string _serverName;
        private readonly string _databaseName;
        private readonly ConventionPack camelConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        private readonly ConventionPack ignoreExtraElementsPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
        private readonly ConventionPack ignoreNullsPack = new ConventionPack { new IgnoreIfNullConvention(true) };
        private readonly MongoClient client;
        public string ServerName => _serverName;
        public string DatabaseName => _databaseName;


        public OrderContext()
        {
            client = new MongoClient(_serverName);
            Database = client.GetDatabase(_databaseName);
        }

        public OrderContext(IOptions<AppSettings> config)
        {
            _serverName = config.Value.CommandServerName;
            _databaseName = config.Value.CommandDatabaseName;

            ConventionPack pack = new ConventionPack
            {
                new IgnoreIfNullConvention(true),
                new IgnoreExtraElementsConvention(true),
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register("defaults", pack, t => true);
            client = new MongoClient(_serverName);
            Database = client.GetDatabase(_databaseName);
        }

        public OrderContext(string serverName, string databaseName)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            MongoClient client = new MongoClient(_serverName);
            ConventionRegistry.Register("CamelCaseConvensions", camelConventionPack, t => true);
            ConventionRegistry.Register("IgnoreExtraElements", ignoreExtraElementsPack, t => true);
            ConventionRegistry.Register("Ignore null values", ignoreNullsPack, t => true);
            Database = client.GetDatabase(_databaseName);
        }
        public IMongoCollection<Order> Orders => Database.GetCollection<Order>("Products");




    }
}
