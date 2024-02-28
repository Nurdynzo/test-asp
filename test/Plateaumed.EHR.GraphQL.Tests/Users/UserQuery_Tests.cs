using System.Threading.Tasks;
using Plateaumed.EHR.Schemas;
using Xunit;

namespace Plateaumed.EHR.GraphQL.Tests.Users
{
    // ReSharper disable once InconsistentNaming
    public class UserQuery_Tests : GraphQLTestBase<MainSchema>
    {
        [Fact(Skip = "This is not is use for now")]
        public async Task Should_Get_Users()
        {
            LoginAsDefaultTenantAdmin();

            const string query = @"
             query MyQuery {
                users (id:2){
                    totalCount
                    items {
                      name
                      surname

                      roles {
                        id
                        name
                        displayName
                      }

                      organizationUnits {
                        id
                        code
                        displayName
                      }
                    }
                  }
             }";


            const string expectedResult = @"{
                ""data"": {
                    ""users"": {
                        ""totalCount"": 1,
                        ""items"": [
                        {
                            ""name"": ""Default"",
                            ""surname"": ""Admin"",
                            ""roles"": [
                            {
                                ""id"": 2,
                                ""name"": ""Admin"",
                                ""displayName"": ""Admin""
                            }
                            ],
                            ""organizationUnits"": []
                        }
                        ]
                    }
                }
            }";

            await AssertQuerySuccessAsync(query, expectedResult);
        }
    }
}
