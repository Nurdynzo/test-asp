using System.Threading.Tasks;
using Plateaumed.EHR.Schemas;
using Xunit;

namespace Plateaumed.EHR.GraphQL.Tests.Roles
{
    // ReSharper disable once InconsistentNaming
    public class RoleQuery_Tests : GraphQLTestBase<MainSchema>
    {
        [Fact(Skip = "This is not is use for now")]
        public async Task Should_Get_Roles()
        {
            //arrange
            LoginAsDefaultTenantAdmin();

            const string query = @"
             query MyQuery {
                roles {
                  id
                  displayName
                }
             }";
        
            var expectedResult = @"{
  ""data"": {
            ""roles"": [
            {
                ""displayName"": ""Admin"",
                ""id"": 2
            },
            {
                ""displayName"": ""User"",
                ""id"": 3
            },
            {
                ""displayName"": ""Doctor"",
                ""id"": 4
            },
            {
                ""displayName"": ""Nurse"",
                ""id"": 5
            },
            {
                ""displayName"": ""Pharmacist"",
                ""id"": 6
            },
            {
                ""displayName"": ""Lab Scientist"",
                ""id"": 7
            },
            {
                ""displayName"": ""Receptionist"",
                ""id"": 8
            }
            ]
        }
    }";
            //act and assert
            await AssertQuerySuccessAsync(query, expectedResult);
        }
        
        
    }

}
