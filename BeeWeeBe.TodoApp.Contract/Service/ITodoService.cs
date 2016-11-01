using System.Collections.Generic;
using System.Threading.Tasks;
using BeeWeeBe.TodoApp.Contract.Dto;

namespace BeeWeeBe.TodoApp.Contract.Service
{
    public interface ITodoService
    {
        Task<TodoDto> Create(TodoDto todo);
        Task Delete(int id);
        Task<TodoDto> Get(int id);
        Task<IEnumerable<TodoDto>> GetAll();
        Task Update(int id, TodoDto todo);
    }
}