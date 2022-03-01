using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Windows;
using Templater.Data;
using Templater.Infrastructure.Interfaces;
using Templater.Infrastructure.Services.InDB;
using Templater.Infrastructure.Services.InMemory;
using Templater.Infrastructure.Services.RabbitMQ;
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


        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<TemplaterDbInitializer>().Initialize();

            var service = Services.GetRequiredService<IRabbitMQService>();

            var testData = new Dictionary<string, string>();

            testData.Add("<Full FIO>", "Петрова Ирина Васильевна");
            testData.Add("<PassportSerial>", "0514");
            testData.Add("<PassportNumber>", "756432");

            var data = new DataIntegrationEvent
            {
                FileName = $"Паспорт.docx",
                TemplateId = "6",
                Data = testData
            };

            service.Publish(data);

            base.OnStartup(e);

        }
    }
}
