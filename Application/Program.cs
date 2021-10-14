using System;
using System.Collections.Generic;
using DependencyInjection;

namespace Application
{
    class Program
    {
        class  A { }
        class B<T> : A { public B(T a) { } }
        class C : A { public C(int list) { } }
        class D : B<int> { public D(List<double> list) : base(10) { } }

        static void Main(string[] args)
        {
            DependenciesConfiguration configuration = new DependenciesConfiguration();
            DependencyProvider provider = new DependencyProvider(configuration);

            try
            {
                //configuration.Register<IEnumerable<int>, List<int>>();
                //configuration.Register<List<A>, List<A>>(true);
                configuration.Register<A, C>();
                configuration.Register<int, int>();
      
                A instance = provider.Resolve<A>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
