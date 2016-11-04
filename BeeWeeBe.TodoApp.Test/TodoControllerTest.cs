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
        private Mock<ITodoService> _mockService;
        private TodoController _sut;
        
        public TodoControllerTest()
        {
          _mockService = new Mock<ITodoService>();
          _sut = new TodoController(_mockService.Object);
        }

        [Fact]
        public async Task GivenATodoController_WhenGet_ThenAListOfTodosIsReturned()
        {
            _mockService.Setup(service => service.GetAll()).Returns(Task.FromResult<IEnumerable<TodoDto>>(new List<TodoDto> { new TodoDto()}));
            var result = await _sut.Get();

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            var data = (IEnumerable<TodoDto>)okObjectResult.Value;
            Assert.Equal(1, data.Count());
        }

        [Fact]
        public async Task GivenATodoController_WhenGetAnExistingTodo_ThenTheTodoIsReturned()
        {
            var id = 123;
            _mockService.Setup(service => service.Get(id)).Returns(Task.FromResult<TodoDto>(new TodoDto
            {
                Id = id
            }));
            var result = await _sut.Get(id);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            var data = (TodoDto)okObjectResult.Value;
            Assert.Equal(id, data.Id);
        }

        [Fact]
        public async Task GivenATodoController_WhenGetATodoThatDoesNotExist_ThenNotFoundIsReturned()
        {
            var id = 123;
            _mockService.Setup(service => service.Get(id)).Returns(Task.FromResult<TodoDto>(default(TodoDto)));
            var result = await _sut.Get(id);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
