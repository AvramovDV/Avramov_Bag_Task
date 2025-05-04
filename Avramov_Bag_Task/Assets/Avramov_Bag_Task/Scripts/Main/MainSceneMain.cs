using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Avramov.Bag
{
    public class MainSceneMain : MonoBehaviour
    {
        private SystemsController _systemsController;

        [Inject]
        private void Construct(List<ISystem> systems, SystemsController systemsController)
        {
            _systemsController = systemsController;
            _systemsController.Init(systems);
        }

        private void Start()
        {
            _systemsController.Activate<BootSystem>();
        }
    }
}
