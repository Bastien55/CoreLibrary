using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
    public class RabbitMQManager
    {
        #region Singleton

        private static RabbitMQManager _instance;

        private static readonly object padLock = new object();

        public static RabbitMQManager Instance 
        { 
            get 
            {
                lock (padLock)
                {
                    if (_instance == null)
                        _instance = new RabbitMQManager();
                }

                return _instance;
            }
        }

        #endregion

        private RabbitMQManager() { }

        public IModel CreateChannel(string hostname, string queue)
        {
            var factory = new ConnectionFactory() { HostName = hostname };

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            return channel;
        }
    }
}
