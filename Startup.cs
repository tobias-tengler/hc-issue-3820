using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChocolate;
using Microsoft.EntityFrameworkCore;


public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddPooledDbContextFactory<DatabaseContext>(b => b.UseSqlServer("Server=localhost;Database=test;User Id=sa;Password=Password!;"));

        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddProjections();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            // By default the GraphQL server is mapped to /graphql
            // This route also provides you with our GraphQL IDE. In order to configure the
            // the GraphQL IDE use endpoints.MapGraphQL().WithToolOptions(...).
            endpoints.MapGraphQL();
        });
    }
}