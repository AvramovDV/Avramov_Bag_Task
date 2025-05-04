using UnityEngine;
using Zenject;

namespace Avramov.Bag
{
    [CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
    public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
    {
        [SerializeField] private BagsProvider _bags;
        [SerializeField] private ItemViewsProvider _items;

        public override void InstallBindings()
        {
            Container.Bind<BagsProvider>().FromInstance(_bags).AsSingle();
            Container.Bind<ItemViewsProvider>().FromInstance(_items).AsSingle();
        }
    }
}