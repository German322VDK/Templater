using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templater.Infrastructure.Interfaces;

namespace Templater.Infrastructure.Services.RabbitMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IConfiguration _configuration;

        public RabbitMQService(IRabbitMQConnection connection,  IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }


    }
}
