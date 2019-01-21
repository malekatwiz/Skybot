using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skybot.Api.Services;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Luis;
using Skybot.Api.Services.Settings;

namespace Skybot.Api
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
            services.AddTransient<ISettings, Settings>();
            services.AddTransient<ILuisService, LuisService>();
            services.AddTransient<IRecognitionService, RecognitionService>();
            services.AddTransient<IIntentFactory, IntentFactory>();
            services.AddTransient<IIntentService, IntentService>();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["Authority"];
                    options.ApiName = "Skybot.Api";
                    options.RequireHttpsMetadata = false;
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
