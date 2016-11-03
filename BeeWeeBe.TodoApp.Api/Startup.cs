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
using System.Threading.Tasks;
using AutoMapper;

namespace BeeWeeBe.TodoApp.Api
{
    public class Startup
    {
        private MapperConfiguration _mapperConfiguration { get; set; }

        public Startup()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp => errorApp.Run(async context => await HandleException(context)));
            app.UseMvc();

            EnsureDatabase(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Filename=./todo.db"));
            services.AddSingleton<IMapper>(sp => _mapperConfiguration.CreateMapper());
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

        private async Task HandleException(HttpContext context)
        {
            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            if (exceptionHandler != null)
            {
                var exception = exceptionHandler.Error;

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    Code = 500,
                    Message = exception.Message
                }), Encoding.UTF8);
            }
        }
    }
}