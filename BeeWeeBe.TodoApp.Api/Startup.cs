using BeeWeeBe.TodoApp.Contract.Service;
using BeeWeeBe.TodoApp.Business.Service;
using BeeWeeBe.TodoApp.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;

namespace BeeWeeBe.TodoApp.Api
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            Code = 500,
                            Message = ex.Message
                        }), Encoding.UTF8);
                    }
                });
            });
            app.UseMvc();

            EnsureDatabase(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Filename=./todo.db"));
            services.AddTransient<ITodoService, TodoService>();
        }

        private void EnsureDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationContext>().Database.EnsureCreated();
                serviceScope.ServiceProvider.GetService<ApplicationContext>().Database.Migrate();
            }
        }
    }
}