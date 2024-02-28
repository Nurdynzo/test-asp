using Abp.AutoMapper;
using Abp.ObjectMapping;
using AutoMapper;

namespace Plateaumed.EHR.Tests
{
    public class AutoMapperTestHelpers
    {
        public static IObjectMapper CreateRealObjectMapper()
        {
            var expression = new MapperConfigurationExpression();
            CustomDtoMapper.CreateMappings(expression);
            var mapperConfiguration = new MapperConfiguration(expression);
            var mapper = new Mapper(mapperConfiguration);
            IObjectMapper objectMapper = new AutoMapperObjectMapper(mapper);
            return objectMapper;
        }
    }
}
