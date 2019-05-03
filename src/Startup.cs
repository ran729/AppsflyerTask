using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AppsflyerTwitter.DAL;
using Consul;
using System;
using System.Linq;

namespace SimilarTwitWeb
{
    public class Startup
    {
        private readonly ConsulClient _consulClient;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var address = Configuration.GetValue<string>("Consul");
            _consulClient = new ConsulClient(o => { o.Address = new Uri(address); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
       
            services.AddDbContext<DatabaseContext>(options =>
                {
                    var serverAddress = ResolveMySqlDbAddress();
                    options.UseMySql(string.Format(Configuration.GetConnectionString("AppsflyerTwitter"), serverAddress));
                }
            );

            services.AddTransient<ITweetRepository, TweetsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }

        private string ResolveMySqlDbAddress()
        {
            var task = _consulClient.Agent.Services();
            task.Wait();
            var services = task.Result;
            var agent = services.Response.Values.First(o => o.Service == "appsflyer-tweeter-db-3306");
            return agent.Address;
        }
    }
}
