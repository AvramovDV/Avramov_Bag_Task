using UnityEngine;
using Zenject;

namespace Avramov.Bag
{
    public class MainSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindModels();
            BindControllers();
            BindSceneObjects();
            BindSystems();
        }

        private void BindModels()
        {
            Container.Bind<InventoryModel>().AsSingle();
        }

        private void BindControllers()
        {
            Container.Bind<SystemsController>().AsSingle();
        }

        private void BindSceneObjects()
        {
            Container.Bind<BagsManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ChooseBagScreen>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ItemsManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameControlScreen>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MessagesManager>().FromComponentInHierarchy().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<BootSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChooseBagSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameControlSystem>().AsSingle();
        }
    }
}
