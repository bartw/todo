using Xunit;
using BeeWeeBe.TodoApp.Business.Entity;

namespace BeeWeeBe.TodoApp.Test.Entity
{
    public class TodoTest
    {
        [Fact]
        public void GivenATodo_WhenUpdate_ThenTheTodoIsUpdated()
        {
            var expectedId = 123;
            var expectedTitle = "expectedTitle";
            var expectedDescription ="expectedDescription";
            var expectedCompleted = true;

            var todo = new Todo
            {
                Id = expectedId,
                Title = "title",
                Description = "description",
                Completed = false
            };

            todo.Update(new Todo
            {
                Id = 234,
                Title = expectedTitle,
                Description = expectedDescription,
                Completed = expectedCompleted
            });

            Assert.Equal(expectedId, todo.Id);
            Assert.Equal(expectedTitle, todo.Title);
            Assert.Equal(expectedDescription, todo.Description);
            Assert.Equal(expectedCompleted, todo.Completed);
        }
    }
}