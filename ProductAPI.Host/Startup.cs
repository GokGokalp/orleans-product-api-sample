using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductAPI.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IClusterClient orleansClient = CreateOrleansClient();
            services.AddSingleton<IClusterClient>(orleansClient);

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Orleans Product API Sample", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
            });
        }

        private IClusterClient CreateOrleansClient()
        {
            var clientBuilder = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options => 
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "ProductAPI";
                });

            var client = clientBuilder.Build();

            client.Connect().Wait();

            return client;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orleans Product API Sample");
            });

            app.UseMvc();
        }
    }
}