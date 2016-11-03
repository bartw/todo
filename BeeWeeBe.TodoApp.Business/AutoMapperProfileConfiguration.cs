using AutoMapper;
using BeeWeeBe.TodoApp.Business.Entity;
using BeeWeeBe.TodoApp.Contract.Dto;

namespace BeeWeeBe.TodoApp.Business
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() : base()
        {
            CreateMap<TodoDto, Todo>();
            CreateMap<Todo, TodoDto>();
        }
    }
}