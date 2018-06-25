using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace ProductAPI.Silo
{
    class Program
    {
        static void Main(string[] args)
        {
                var siloBuilder = new SiloHostBuilder()
                        .UseLocalhostClustering()
                        .UseDashboard(options => { })
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "ProductAPI";
                        })
                        .Configure<EndpointOptions>(options =>
                            options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureLogging(logging => logging.AddConsole());

                using (var host = siloBuilder.Build())
                {
                    host.StartAsync().Wait();
                    Console.ReadLine();
                }
        }
    }
}