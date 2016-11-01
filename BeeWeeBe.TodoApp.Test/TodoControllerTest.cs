using Xunit;
using BeeWeeBe.TodoApp.Contract.Dto;
using BeeWeeBe.TodoApp.Contract.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BeeWeeBe.TodoApp.Api.ApiController;
using System.Linq;

namespace BeeWeeBe.TodoApp.Test
{
    public class TodoControllerTest
    {
        [Fact]
        public async Task GivenATodoController_WhenGet_ThenAListOfTodosIsReturned()
        {
            var mockService = new Mock<ITodoService>();
            mockService.Setup(service => service.GetAll()).Returns(Task.FromResult<IEnumerable<TodoDto>>(new List<TodoDto> { new TodoDto()}));
            var controller = new TodoController(mockService.Object);

            var result = await controller.Get();

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            var data = ((IEnumerable<TodoDto>)okObjectResult.Value);
            Assert.Equal(1, data.Count());
        }
    }
}
