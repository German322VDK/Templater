﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Templater.Data;
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
            ??= Host.CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureServices(ConfigureServices)
            .Build();

        public static IServiceProvider Services => Hosting.Services;

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>(); //создаём объект MainWindowViewModel 1 раз

            services.AddDbContext<TeplaterSQLDB>();

            services.AddTransient<TemplaterDbInitializer>();

            //services.AddTransient<IMailService, DebugMailService>(); //создаём объект IMailService(DebugMailService) каждый раз новые

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<TemplaterDbInitializer>().Initialize();
            base.OnStartup(e);

        }
    }
}
