using System;
using System.Collections.Generic;

namespace Avramov.Bag
{
    public class SystemsController
    {
        private Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

        public void Init(List<ISystem> systems)
        {
            foreach (ISystem system in systems) 
                _systems.Add(system.GetType(), system);
        }

        public void Activate<T>()
        {
            _systems[typeof(T)].Activate();
        }

        public void Deactivate<T>()
        {
            _systems[typeof(T)].Deactivate();
        }
    }
}
