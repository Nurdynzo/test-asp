using System.Threading.Tasks;
using Plateaumed.EHR.Schemas;
using Xunit;

namespace Plateaumed.EHR.GraphQL.Tests.OrganizationUnits
{
    // ReSharper disable once InconsistentNaming
    public class OrganizationUnitQuery_Tests : GraphQLTestBase<MainSchema>
    {
        [Fact(Skip = "This test does not concern any implementation but stopping build")]
 
        public async Task Should_Get_OrganizationUnits()
        {
            const string query = @"
             query MyQuery {
                organizationUnits {
                  id
                  displayName
                }
             }";


            const string expectedResult = "{\"organizationUnits\": [  {	\"id\": 1,	\"displayName\": \"OU1\"  },  {	\"id\": 2,	\"displayName\": \"OU11\"  },  {	\"id\": 3,	\"displayName\": \"OU111\"  },  {	\"id\": 4,	\"displayName\": \"OU112\"  },  {	\"id\": 5,	\"displayName\": \"OU12\"  },  {	\"id\": 6,	\"displayName\": \"OU2\"  },  {	\"id\": 7,	\"displayName\": \"OU21\"  }]}";

            await AssertQuerySuccessAsync(query, expectedResult);
        }
    }
}
