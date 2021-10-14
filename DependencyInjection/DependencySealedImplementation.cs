using System;

namespace DependencyInjection
{
    public class DependencySealedImplementation : DependencyImplementation
    {
        private object _instance;

        public DependencySealedImplementation(Type implementationType) : base(implementationType)
        {

        }

        public override object GetInstance(DependencyProvider provider)
        {
            if (_instance == null)
            {
                _instance = base.GetInstance(provider);
            }
            return _instance;
        }
    }
}
