using UnityEngine;

namespace Avramov.Bag
{
    public class ChooseBagSystem : BaseSystem
    {
        private BagsManager _bagsManager;
        private ChooseBagScreen _screen;
        private SystemsController _systemsController;
        private BagsProvider _bagProvider;
        private InventoryModel _inventoryModel;

        private int _currentBag;

        public ChooseBagSystem(BagsManager bagsManager, ChooseBagScreen screen, SystemsController systemsController, BagsProvider bagProvider, InventoryModel inventoryModel)
        {
            _bagsManager = bagsManager;
            _screen = screen;
            _systemsController = systemsController;
            _bagProvider = bagProvider;
            _inventoryModel = inventoryModel;
        }

        protected override void Activated()
        {
            _screen.NextMyButton.ClickEvent += NextBag;
            _screen.PreviousMyButton.ClickEvent += PreviousBag;
            _screen.SelectMyButton.ClickEvent += SelectBag;
            _screen.ExitMyButton.ClickEvent += Exit;

            SetupBag(true, false);
            SetupScreen();
            _screen.gameObject.SetActive(true);
        }

        protected override void Deactivated()
        {
            _screen.NextMyButton.ClickEvent -= NextBag;
            _screen.PreviousMyButton.ClickEvent -= PreviousBag;
            _screen.SelectMyButton.ClickEvent -= SelectBag;
            _screen.ExitMyButton.ClickEvent -= Exit;

            _screen.gameObject.SetActive(false);
        }

        private void SetupBag(bool isNext, bool withAnimation)
        {
            _bagsManager.ShowBag(_bagProvider.Bags[_currentBag], isNext, withAnimation);
        }

        private void SetupScreen()
        {
            _screen.BagsText.text = $"{_currentBag + 1}/{_bagProvider.Bags.Count}";
            _screen.NextMyButton.gameObject.SetActive(_currentBag < _bagProvider.Bags.Count - 1);
            _screen.PreviousMyButton.gameObject.SetActive(_currentBag > 0);
        }

        private void NextBag()
        {
            _currentBag++;
            _currentBag = _currentBag >= _bagProvider.Bags.Count ? 0 : _currentBag;
            SetupBag(true, true);
            SetupScreen();
        }


        private void PreviousBag()
        {
            _currentBag--;
            _currentBag = _currentBag < 0 ? _bagProvider.Bags.Count - 1 : _currentBag;
            SetupBag(false, true);
            SetupScreen();
        }

        private void SelectBag()
        {
            _inventoryModel.InitBag(_bagsManager.Bag.GetCells());
            _systemsController.SetupGameSystems();
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
