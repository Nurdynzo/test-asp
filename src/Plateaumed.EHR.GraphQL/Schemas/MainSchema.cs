using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using Plateaumed.EHR.Queries.Container;
using System;

namespace Plateaumed.EHR.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}