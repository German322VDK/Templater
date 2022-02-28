using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Windows;
using Templater.Data;
using Templater.Infrastructure.Interfaces;
using Templater.Infrastructure.Services.InDB;
using Templater.Infrastructure.Services.InMemory;
using Templater.Infrastructure.Services.RabbitMQ;
using Templater.IntegrationEvents.Events;
using Templater.IntegrationEvents.Handlers;
using Templater.ViewModels;
using Templator.DTO.DTOModels;
using Templator.DTO.Models;
using Teplater.SQLite.Context;

namespace Templater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _hosting;

        public static IHost Hosting => _hosting
            ??= CreateHostBuilder(Environment.GetCommandLineArgs())
            .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
              .ConfigureAppConfiguration(opt => opt.AddJsonFile("appsettings.json", false, true))
              .ConfigureServices(ConfigureServices)
           ;

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<AdministratorViewModel>();

            services.AddSingleton<PrintOperatorViewModel>();

            services.AddSingleton<DataOperatorViewModel>();

            services.AddSingleton<DocumentsRepository>();

            services.AddDbContext<TeplaterSQLDB>();

            services.AddTransient<TemplaterDbInitializer>();

            services.AddTransient<IStore<Document>, DocumentDBStore>();

            services.AddTransient<IStore<Template>, TemplateDBStore>();

            services.AddSingleton<IConnectionFactory, ConnectionFactory>( sp => 
            {

                var factory = new ConnectionFactory()
                {
                    HostName = host.Configuration.GetSection("RabbitMQ")["HostName"],
                    UserName = host.Configuration.GetSection("RabbitMQ")["Login"],
                    Password = host.Configuration.GetSection("RabbitMQ")["Password"],
                    Port = 5671,
                    Uri = new Uri(host.Configuration.GetSection("RabbitMQ")["Uri"])
                };

                factory.Ssl.Enabled = true;

                return factory;
            });

            services.AddSingleton<IRabbitMQService, RabbitMQService>();

            //services.AddTransient<IRabbitMQConnection, RabbitMQConnection>();

            //services.AddTransient<GetDataIntegrationEventHandler>();

            //services.AddTransient<IStore<Solution>, SolutionDBStore>();

            //services.AddTransient<IMailService, DebugMailService>(); //создаём объект IMailService(DebugMailService) каждый раз новые

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<TemplaterDbInitializer>().Initialize();

           

            var service = Services.GetRequiredService<IRabbitMQService>();

            //var testData = new Dictionary<string, string>();

            //for (int i = 0; i < 2; i++)
            //{
            //    testData.Clear();
            //    testData.Add($"{i}", $"test-{i}");

            //    var data = new DataIntegrationEvent
            //    {
            //        FileName = $"File{i}",
            //        Data = testData
            //    };


            //    service.Publish(data);

            //}


            //service.Subscribe( );

            base.OnStartup(e);


        }
    }
}
