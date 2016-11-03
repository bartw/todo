using Microsoft.AspNetCore.Mvc;
using BeeWeeBe.TodoApp.Contract.Service;
using BeeWeeBe.TodoApp.Contract.Dto;
using System.Threading.Tasks;

namespace BeeWeeBe.TodoApp.Api.ApiController
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var todos = await _todoService.GetAll();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todo = await _todoService.Get(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TodoDto todoToCreate)
        {
            var createdTodo = await _todoService.Create(todoToCreate);
            return CreatedAtAction("Get", new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]TodoDto todoToUpdate)
        {
            await _todoService.Update(id, todoToUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoService.Delete(id);
            return Ok();
        }
    }
}