using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DependencyInjection.Test
{
    [TestClass]
    public class MainTest
    {
        abstract class A { }
        class B : A { }
        class C : A { public C(int value) { } }
        class D<T> : A { public D(T value) { } }
        class E : C { public E(IEnumerable<int> collection) : base(10) { } }

        [TestMethod]
        public void ResolveA_ReturnB()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            DependencyProvider provider = new DependencyProvider(configuration);

            configuration.Register<A, B>();
            configuration.Register<B, B>();

            var obj = provider.Resolve<A>();

            Assert.IsTrue(obj is B);
        }

        [TestMethod]
        public void ResolveA_ReturnC()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            DependencyProvider provider = new DependencyProvider(configuration);

            configuration.Register<A, C>();
            configuration.Register<int, int>();

            var obj = provider.Resolve<A>();

            Assert.IsTrue(obj is C);
        }

        [TestMethod]
        public void ResolveA_ReturnD()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            DependencyProvider provider = new DependencyProvider(configuration);

            configuration.Register<A, D<int>>();
            configuration.Register<int, int>();

            var obj = provider.Resolve<A>();

            Assert.IsTrue(obj is D<int>);
        }

        [TestMethod]
        public void ResolveA_ReturnE()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            DependencyProvider provider = new DependencyProvider(configuration);

            configuration.Register<A, E>();
            configuration.Register<IEnumerable<int>, List<int>>();

            var obj = provider.Resolve<A>();

            Assert.IsTrue(obj is E);
        }

        [TestMethod]
        public void ResolveAllA_ReturnArrayA_Length4()
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            DependencyProvider provider = new DependencyProvider(configuration);

            configuration.Register<A, B>();
            configuration.Register<A, C>();
            configuration.Register<A, D<float>>();
            configuration.Register<A, E>();
            configuration.Register<int, int>();
            configuration.Register<float, float>();
            configuration.Register<IEnumerable<int>, List<int>>();

            var obj = provider.ResolveAll<A>();

            Assert.IsTrue(obj.Length == 4);
        }
    }
}
