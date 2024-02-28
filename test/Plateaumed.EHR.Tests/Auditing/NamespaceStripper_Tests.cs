﻿using Plateaumed.EHR.Auditing;
using Plateaumed.EHR.Test.Base;
using Shouldly;
using Xunit;

namespace Plateaumed.EHR.Tests.Auditing
{
    // ReSharper disable once InconsistentNaming
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("Plateaumed.EHR.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("Plateaumed.EHR.Auditing.GenericEntityService`1[[Plateaumed.EHR.Storage.BinaryObject, Plateaumed.EHR.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("Plateaumed.EHR.Auditing.XEntityService`1[Plateaumed.EHR.Auditing.AService`5[[Plateaumed.EHR.Storage.BinaryObject, Plateaumed.EHR.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[Plateaumed.EHR.Storage.TestObject, Plateaumed.EHR.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
