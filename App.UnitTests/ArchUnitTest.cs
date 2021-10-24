using System;
using Xunit;
using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

using static ArchUnitNET.Fluent.ArchRuleDefinition;
using ArchUnitNET.Fluent;

namespace App.UnitTests
{
    public class ArchUnitTest
    {
        // TIP: load your architecture once at the start to maximize performance of your tests
        private static readonly Architecture Architecture =
            new ArchLoader().LoadAssemblies(typeof(ExampleClass).Assembly, typeof(ForbiddenClass).Assembly).Build();
        // replace <ExampleClass> and <ForbiddenClass> with classes from the assemblies you want to test

        //declare variables you'll use throughout your tests up here
        //use As() to give them a custom description
        private readonly IObjectProvider<IType> ExampleLayer =
            Types().That().ResideInAssembly("ExampleAssembly").As("Example Layer");

        private readonly IObjectProvider<Class> ExampleClasses =
            Classes().That().ImplementInterface("IExampleInterface").As("Example Classes");

        private readonly IObjectProvider<IType> ForbiddenLayer =
            Types().That().ResideInNamespace("App.Core").As("Forbidden Layer");

        private readonly IObjectProvider<Interface> ForbiddenInterfaces =
            Interfaces().That().HaveFullNameContaining("forbidden").As("Forbidden Interfaces");

        //write some tests
        [Fact]
        public void TypesShouldBeInCorrectLayer()
        {
            //you can use the fluent API to write your own rules
            IArchRule exampleClassesShouldBeInExampleLayer =
                Classes().That().Are(ExampleClasses).Should().Be(ExampleLayer);
            IArchRule forbiddenInterfacesShouldBeInForbiddenLayer =
                Interfaces().That().Are(ForbiddenInterfaces).Should().Be(ForbiddenLayer);

            //check if your architecture fulfils your rules
            exampleClassesShouldBeInExampleLayer.Evaluate(Architecture);
            forbiddenInterfacesShouldBeInForbiddenLayer.Evaluate(Architecture);

            //you can also combine your rules
            IArchRule combinedArchRule =
                exampleClassesShouldBeInExampleLayer.And(forbiddenInterfacesShouldBeInForbiddenLayer);
            combinedArchRule.Evaluate(Architecture);
        }


        [Fact]
        public void ExampleLayerShouldNotAccessForbiddenLayer()
        {
            //you can give your rules a custom reason, which is displayed when it fails (together with the types that failed the rule)
            IArchRule exampleLayerShouldNotAccessForbiddenLayer = Types().That().Are(ExampleLayer).Should()
                .NotDependOnAny(ForbiddenLayer).Because("it's forbidden");
            exampleLayerShouldNotAccessForbiddenLayer.Evaluate(Architecture);
        }

        [Fact]
        public void ForbiddenClassesShouldHaveCorrectName()
        {
            Classes().That().AreAssignableTo(ForbiddenInterfaces).Should().HaveNameContaining("App.Core")
                .Evaluate(Architecture);
        }

        [Fact]
        public void ExampleClassesShouldNotCallForbiddenMethods()
        {
            Classes().That().Are(ExampleClasses).Should().NotCallAny(
                    MethodMembers().That().AreDeclaredIn(ForbiddenLayer).Or().HaveNameContaining("forbidden"))
                .Evaluate(Architecture);
        }
    }
}

internal class ExampleClass
{
}

internal class ForbiddenClass
{
}