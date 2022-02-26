using Autofac.Extensions.DependencyInjection;
using EventBus.Base.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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
              .UseServiceProviderFactory(new AutofacServiceProviderFactory())
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

            //services.AddTransient<IRabbitMQService, RabbitMQService>();

            //services.AddTransient<IRabbitMQConnection, RabbitMQConnection>();

            var rabbitMqOptions = host.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();

            services.AddRabbitMqConnection(rabbitMqOptions);
            services.AddRabbitMqRegistration(rabbitMqOptions);
            services.AddEventBusHandling(EventBusExtension.GetHandlers());

            //services.AddTransient<IStore<Solution>, SolutionDBStore>();

            //services.AddTransient<IMailService, DebugMailService>(); //создаём объект IMailService(DebugMailService) каждый раз новые

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<TemplaterDbInitializer>().Initialize();

            var eventBus = Services.GetRequiredService<IRabbitMQService>(); 
           // eventBus.Subscribe<GetDataIntegrationEvent, GetDataIntegrationEventHandler>();

            base.OnStartup(e);

        }
    }
}
