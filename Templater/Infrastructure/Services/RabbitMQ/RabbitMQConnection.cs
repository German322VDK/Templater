using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using Templater.Infrastructure.Interfaces;

namespace Templater.Infrastructure.Services.RabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConfiguration _configuration;
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        private readonly object _Lock = new();

        public RabbitMQConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(_configuration["RabbitMQ:Uri"]),
                UserName = _configuration["RabbitMQ:Login"],
                Password = _configuration["RabbitMQ:Password"]
            };
        }

        public bool IsConnected => _connection is not null && _connection.IsOpen && !_disposed;

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                string e = "No RabbitMQ connections are available to perform this action";

                throw new InvalidOperationException(e);
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) 
                return;
            _disposed = true;

            _connection.Dispose();
        }

        public bool TryConnect()
        {

            lock (_Lock)
            {
                _connection = _connectionFactory.CreateConnection();
            }

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;
            }

            return true;
        }

        private void OnConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) 
                return;
            
            TryConnect();
        }

        private void OnCallbackException(object? sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) 
                return;

            TryConnect();
        }

        private void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            if (_disposed) 
                return;

            TryConnect();
        }
    }
}
