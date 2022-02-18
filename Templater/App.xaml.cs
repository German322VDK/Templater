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

            services.AddTransient<IRabbitMQService, RabbitMQService>();

            services.AddTransient<IRabbitMQConnection, RabbitMQConnection>();

            //services.AddTransient<IStore<Solution>, SolutionDBStore>();

            //services.AddTransient<IMailService, DebugMailService>(); //создаём объект IMailService(DebugMailService) каждый раз новые

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<TemplaterDbInitializer>().Initialize();
            base.OnStartup(e);

        }
    }
}
