using UnityEngine;

namespace Avramov.Bag
{
    public class BootSystem : BaseSystem
    {
        private SystemsController _systemsController;

        public BootSystem(SystemsController systemsController)
        {
            _systemsController = systemsController;
        }

        protected override void Activated()
        {
            _systemsController.Activate<ChooseBagSystem>();
        }

        protected override void Deactivated()
        {
            
        }
    }
}
