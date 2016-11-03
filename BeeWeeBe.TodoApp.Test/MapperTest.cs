using Xunit;
using AutoMapper;
using BeeWeeBe.TodoApp.Business;

namespace BeeWeeBe.TodoApp.Test
{
    public class MapperTest
    {
        [Fact]
        public void Test()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
