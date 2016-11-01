using BeeWeeBe.TodoApp.Business.Entity;
using Microsoft.EntityFrameworkCore;

namespace BeeWeeBe.TodoApp.Business
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Todo>().HasKey(todo => todo.Id);
            builder.Entity<Todo>().Property(todo => todo.Id).ValueGeneratedOnAdd();
            builder.Entity<Todo>().Property(todo => todo.Title).IsRequired();
            builder.Entity<Todo>().Property(todo => todo.Completed).IsRequired();
        }
    }
}
