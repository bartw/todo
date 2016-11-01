using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeeWeeBe.TodoApp.Business.Entity;
using BeeWeeBe.TodoApp.Contract.Dto;
using BeeWeeBe.TodoApp.Contract.Service;
using Microsoft.EntityFrameworkCore;

namespace BeeWeeBe.TodoApp.Business.Service
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationContext _context;

        public TodoService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<TodoDto> Create(TodoDto todoDto)
        {
            var todo = Map(todoDto);
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return Map(todo);
        }

        public async Task Delete(int id)
        {
            var todo = await Find(id);
            if (todo == null)
            {
                throw new Exception("Not found");
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoDto> Get(int id)
        {
            return Map(await Find(id));
        }

        public async Task<IEnumerable<TodoDto>> GetAll()
        {
            var todos = await _context.Todos.ToListAsync();
            return todos.Select(todo => Map(todo));
        }

        public async Task Update(int id, TodoDto todo)
        {
            var existingTodo = await Find(id);

            if (existingTodo == null)
            {
                throw new Exception("Not found");
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.Completed = todo.Completed;
            await _context.SaveChangesAsync();
        }

        private async Task<Todo> Find(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        private TodoDto Map(Todo todo)
        {
            if (todo == null)
            {
                return null;
            }

            return new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Completed = todo.Completed
            };
        }

        private Todo Map(TodoDto todo)
        {
            if (todo == null)
            {
                return null;
            }
            
            return new Todo
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Completed = todo.Completed
            };
        }
    }
}