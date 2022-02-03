using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Templater.Data;
using Templater.Infrastructure.Services.InDB;
using Templater.ViewModels;
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
            services.AddSingleton<MainWindowViewModel>(); //создаём объект MainWindowViewModel 1 раз

            services.AddDbContext<TeplaterSQLDB>();

            services.AddTransient<TemplaterDbInitializer>();

            services.AddTransient<DocumentDBStore>();

            services.AddTransient<TemplateDBStore>();

            //services.AddTransient<IMailService, DebugMailService>(); //создаём объект IMailService(DebugMailService) каждый раз новые

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<TemplaterDbInitializer>().Initialize();
            base.OnStartup(e);

        }
    }
}
