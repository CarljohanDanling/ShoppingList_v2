using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ShoppingListWebApi.Model;
using Newtonsoft.Json;
using ShoppingListWebApi.Service;

namespace ShoppingListWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Prevents returned broken JSON.
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // Configuring DI-for database access.
            services.AddDbContext<ShoppingListContext>(options =>
                options.UseSqlServer(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=ShoppingList;Integrated Security=True"));

            services.AddTransient<IRepository, Repository>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
