using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BeeWeeBe.TodoApp.Business.Entity;
using BeeWeeBe.TodoApp.Contract.Dto;
using BeeWeeBe.TodoApp.Contract.Service;
using Microsoft.EntityFrameworkCore;

namespace BeeWeeBe.TodoApp.Business.Service
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public TodoService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TodoDto> Create(TodoDto todoDto)
        {
            var todo = _mapper.Map<Todo>(todoDto);
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return _mapper.Map<TodoDto>(todo);
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
            return _mapper.Map<TodoDto>(await Find(id));
        }

        public async Task<IEnumerable<TodoDto>> GetAll()
        {
            var todos = await _context.Todos.ToListAsync();
            return _mapper.Map<IEnumerable<TodoDto>>(todos);
        }

        public async Task Update(int id, TodoDto todoDto)
        {
            var existingTodo = await Find(id);
            if (existingTodo == null)
            {
                throw new Exception("Not found");
            }
            var todo = _mapper.Map<Todo>(todoDto);
            existingTodo.Update(todo);
            await _context.SaveChangesAsync();
        }

        private async Task<Todo> Find(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}