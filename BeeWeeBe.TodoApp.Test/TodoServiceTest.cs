using Xunit;
using BeeWeeBe.TodoApp.Contract.Dto;
using BeeWeeBe.TodoApp.Business;
using BeeWeeBe.TodoApp.Business.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace BeeWeeBe.TodoApp.Test
{
    public class TodoServiceTest
    {
        private static DbContextOptions<ApplicationContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            builder.UseInMemoryDatabase().UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }

        [Fact]
        public async Task GivenAValidTodoDto_WhenCreate_ThenANewTodoIsCreated()
        {
            var options = CreateNewContextOptions();

            var todo = new TodoDto
            {
                Title = "Title",
                Description = "Description",
                Completed = true
            };

            using (var context = new ApplicationContext(options))
            {
                var service = new TodoService(context);

                var createdTodo = await service.Create(todo);

                Assert.True(createdTodo.Id > 0);
                Assert.Equal(todo.Title, createdTodo.Title);
                Assert.Equal(todo.Description, createdTodo.Description);
                Assert.Equal(todo.Completed, createdTodo.Completed);
            }

            using (var context = new ApplicationContext(options))
            {
                Assert.Equal(1, context.Todos.Count());
                var createdTodo = context.Todos.First();
                Assert.True(createdTodo.Id > 0);
                Assert.Equal(todo.Title, createdTodo.Title);
                Assert.Equal(todo.Description, createdTodo.Description);
                Assert.Equal(todo.Completed, createdTodo.Completed);
            }
        }
    }
}
