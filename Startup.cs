using System;
using System.Net.Http;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace WeatherApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpClient<IOpenWeatherApiService, OpenWeatherApiService>()
                    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    }))
                    .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(
                        handledEventsAllowedBeforeBreaking: 3,
                        durationOfBreak: TimeSpan.FromSeconds(30)
                    ));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddErrorFilter(error => {
                    Console.WriteLine(error.Exception);
                    return error;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints
                        .MapGraphQL()
                        .WithOptions(
                            new GraphQLServerOptions
                            {
                                Tool = { Enable = _env.IsDevelopment() }
                            }
                        );

                    endpoints.MapGet("/", async context =>
                    {
                        await context.Response.WriteAsync("Incorrect usage: This is a graphql server");
                    });
                });
        }
    }
}
