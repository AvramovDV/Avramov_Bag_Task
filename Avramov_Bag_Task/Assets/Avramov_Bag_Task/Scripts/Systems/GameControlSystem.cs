using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Avramov.Bag
{
    public class GameControlSystem : BaseSystem
    {
        private InventoryModel _inventoryModel;
        private ItemsManager _itemsManager;
        private GameControlScreen _screen;
        private ItemViewsProvider _itemsProvider;
        private BagsManager _bagManager;
        private MessagesManager _messagesManager;
        private SystemsController _systemsController;

        private List<CellData> _currentCells = new List<CellData>();

        private ItemContainerView _draggingItem;

        public GameControlSystem(
            InventoryModel inventoryModel,
            ItemsManager itemsManager,
            GameControlScreen gameControlScreen,
            ItemViewsProvider itemsProvider,
            BagsManager bagManager,
            MessagesManager messagesManager,
            SystemsController systemsController)
        {
            _inventoryModel = inventoryModel;
            _itemsManager = itemsManager;
            _screen = gameControlScreen;
            _itemsProvider = itemsProvider;
            _bagManager = bagManager;
            _messagesManager = messagesManager;
            _systemsController = systemsController;
        }

        protected override void Activated()
        {
            _itemsManager.BeginDragEvent += BeginDrag;
            _itemsManager.DragEvent += Drag;
            _itemsManager.EndDragEvent += EndDrag;
            _itemsManager.DropEvent += Drop;

            _screen.BackButton.ClickEvent += Back;
            _screen.PlayButton.ClickEvent += UpdateItems;

            UpdateItems();
            _screen.gameObject.SetActive(true);
        }

        protected override void Deactivated()
        {
            _itemsManager.BeginDragEvent -= BeginDrag;
            _itemsManager.DragEvent -= Drag;
            _itemsManager.EndDragEvent -= EndDrag;
            _itemsManager.DropEvent -= Drop;

            _screen.BackButton.ClickEvent -= Back;
            _screen.PlayButton.ClickEvent -= UpdateItems;
            _screen.gameObject.SetActive(false);
        }

        private void Back()
        {
            _itemsManager.Clear();
            _inventoryModel.Clear();
            _systemsController.SetupChooseBagSystems();
        }

        private void UpdateItems()
        {
            List<Item> items = _itemsProvider.GenerateItems();
            List<Item> currentFreeItems = _inventoryModel.FreeItems.ToList();
            _itemsManager.DestroyItems(currentFreeItems);
            _inventoryModel.SetupFreeItems(items);
            _itemsManager.CreateItems(items);
            PlaceFreeItems(false);
        }

        private void BeginDrag(ItemContainerView arg1, PointerEventData arg2)
        {
            arg1.RectTransform.SetParent(_itemsManager.transform);
            _draggingItem = arg1;
        }

        private void Drag(ItemContainerView arg1, PointerEventData arg2)
        {
            arg1.transform.position = arg2.position;
            DefineCells(arg1);
            SetCellsColors(arg1);
            SetItemsAnimation();
        }

        private void EndDrag(ItemContainerView arg1, PointerEventData arg2)
        {
            TryPutIn(arg1);
            PlaceFreeItems(true);
            SetCellsColors(null);
            _draggingItem = null;
        }

        private void Drop(ItemContainerView arg1, PointerEventData arg2)
        {
            if (_draggingItem == null)
                return;

            Item targetItem = arg1.Item;
            Item droppedItem = _draggingItem.Item;

            if(CanMerge(targetItem, droppedItem))
            {
                targetItem.LevelUp();
                arg1.ShowItem(targetItem);
                _itemsManager.DestroyItem(droppedItem);
                _inventoryModel.RemoveItem(droppedItem);
                PlaceFreeItems(true);
                SetCellsColors(null);
                _draggingItem = null;
                _messagesManager.ShowMessage("Level Up!", arg1.transform.position);
            }
        }

        private void PlaceFreeItems(bool withAnimation)
        {
            var items = _inventoryModel.FreeItems;
            var views = items.Select(x => _itemsManager.GetView(x).RectTransform).ToList();
            _itemsManager.FreeItemsLayout.PlaceItems(views, withAnimation);
            _itemsManager.StopAnimation();
        }

        private void DefineCells(ItemContainerView item)
        {
            var indexes = item.CellPoints.Select(x => _bagManager.Bag.GetIndex(x.position)).ToList();
            _currentCells = _inventoryModel.GetCells(indexes);
        }

        private void SetCellsColors(ItemContainerView item)
        {
            CellStateEnum cellStateEnum = GetCellState(item);
            _bagManager.Bag.SetupCellsColors(_currentCells, cellStateEnum);
        }

        private CellStateEnum GetCellState(ItemContainerView item)
        {
            if(item == null)
                return CellStateEnum.None;

            if(_currentCells.Count == 0)
                return CellStateEnum.None;

            if (_currentCells.Count == item.CellPoints.Count)
                return CellStateEnum.Green;

            if(_currentCells.Any(x => x.Item != null))
                return CellStateEnum.Red;

            return CellStateEnum.Orange;
        }

        private void SetItemsAnimation()
        {
            var bagItems = _inventoryModel.BagItems;
            foreach(var item in bagItems)
            {
                var view = _itemsManager.GetView(item);
                view.SetAnimation(_currentCells.Any(x => x.Item == item) && item != _draggingItem?.Item);
            }
        }

        private void TryPutIn(ItemContainerView item)
        {
            var data = item.Item;

            if (_currentCells.Count != item.CellPoints.Count)
            {
                TryPutOut(data);
                return;
            }

            Vector2Int targeCellIndex = _bagManager.Bag.GetIndex(item.CellPoints[0].position);
            _inventoryModel.PutInItem(data, _currentCells);
            Vector3 position = _bagManager.Bag.GetPosition(targeCellIndex);
            item.MoveToBag(position);
        }

        private void TryPutOut(Item item)
        {
            _inventoryModel.PutOutItem(item);
            var view = _itemsManager.GetView(item);
            view.SetAnimation(false);
        }

        private bool CanMerge(Item item1, Item item2)
        {
            return item1.Level < _itemsProvider.MaxLevel && item1.Type == item2.Type && item1.Level == item2.Level;
        }
    }
}
