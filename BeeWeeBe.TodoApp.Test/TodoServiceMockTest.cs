using Xunit;
using BeeWeeBe.TodoApp.Contract.Dto;
using BeeWeeBe.TodoApp.Business;
using BeeWeeBe.TodoApp.Business.Service;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Moq;
using BeeWeeBe.TodoApp.Business.Entity;
using System.Threading;

namespace BeeWeeBe.TodoApp.Test
{
    public class TodoServiceMockTest
    {
        [Fact]
        public async Task Test()
        {
            var todo = new TodoDto
            {
                Title = "Title",
                Description = "Description",
                Completed = true
            };


            var mockOptions = new Mock<DbContextOptions<ApplicationContext>>();
            mockOptions.Setup(m => m.ContextType).Returns(typeof(ApplicationContext));
            var mockDbSet = new Mock<DbSet<Todo>>();
            var mockContext = new Mock<ApplicationContext>(mockOptions.Object); 
            mockContext.Setup(m => m.Todos).Returns(mockDbSet.Object); 
            mockContext.Setup(m => m.SaveChangesAsync(default(CancellationToken))).Returns(() => Task.Run(() => 1)).Verifiable();
 
            var service = new TodoService(mockContext.Object); 
            var createdTodo = await service.Create(todo); 
 
            mockDbSet.Verify(m => m.Add(It.IsAny<Todo>()), Times.Once()); 
            mockContext.Verify(m => m.SaveChangesAsync(default(CancellationToken)), Times.Once()); 
        }
    }
}
