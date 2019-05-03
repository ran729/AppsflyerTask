using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AppsflyerTwitter.DAL;
using Consul;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SimilarTwitWeb
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var task = GetAddressFromConsul();
            task.Wait();
            var server = task.Result;

            services.AddDbContext<DatabaseContext>(options =>
                { 
                    options.UseMySql($"Server={server};Database=AppsFlyerTweeter;Uid=root;");
                }
            );

            services.AddTransient<ITweetRepository, TweetsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }

        private async Task<string> GetAddressFromConsul()
        {
            var address = Configuration.GetValue<string>("Consul");
            var consulClient = new ConsulClient(o => { o.Address = new Uri(address); });
            var services = await consulClient.Agent.Services();
            var agent = services.Response.Values.First(o => o.Service == "appsflyer-tweeter-db-3306");
            return agent.Address;
        }

    }
}
