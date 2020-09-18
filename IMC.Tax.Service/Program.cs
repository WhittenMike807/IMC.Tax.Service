using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace IMC.Tax.Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("IMC Tax Calculation Service");
            Console.WriteLine("============================================================================");
            Console.WriteLine();

            IServiceCollection services = ConfigureServices();
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<ConsoleApplication>().Run();
            Console.ReadKey(true);
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            IConfiguration config = LoadConfiguration();
            services.AddSingleton(config);
            services.AddHttpClient<ITaxJarClient, TaxJarClient>();
            services.AddTransient<ConsoleApplication>();

            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
