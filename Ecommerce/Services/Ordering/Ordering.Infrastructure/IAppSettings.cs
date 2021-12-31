using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public interface IAppSettings
    {
        string CommandServerName { get; set; }
        string CommandDatabaseName { get; set; }
        string QueryServerName { get; set; }
        string QueryDatabaseName { get; set; }
        string EventStoreConnectionString { get; set; }
        string EventStoreStreamName { get; set; }
        string AzureServiceBus { get; set; }
    }
    public class AppSettings : IAppSettings
    {
        public string CommandServerName { get; set; }
        public string CommandDatabaseName { get; set; }
        public string QueryServerName { get; set; }
        public string QueryDatabaseName { get; set; }
        public string EventStoreConnectionString { get; set; }
        public string EventStoreStreamName { get; set; }
        public string AzureServiceBus { get; set; }
    }
}
