using System.Collections.Generic;
using UnityEngine;

namespace Avramov.Bag
{
    public class InventoryModel
    {
        private List<CellData> _cells;

        private Dictionary<Item, List<CellData>> _itemToCellsDict = new Dictionary<Item, List<CellData>>();

        private List<Item> _freeItems = new List<Item>();
        private List<Item> _bagItems = new List<Item>();

        public IReadOnlyList<Item> FreeItems => _freeItems;
        public IReadOnlyList<Item> BagItems => _bagItems;

        public void InitBag(List<CellData> cells)
        {
            _cells = cells;
        }

        public List<CellData> GetCells(List<Vector2Int> cells)
        {
            return _cells.FindAll(x => cells.Contains(x.Index));
        }

        public CellData GetCell(Vector2Int index)
        {
            return _cells.Find(x => x.Index == index);
        }

        public void PutInItem(Item item, List<CellData> targetCells)
        {
            foreach (CellData cell in targetCells)
                PutOutItem(cell.Item);

            if (_itemToCellsDict.ContainsKey(item))
                PutOutItem(item);

            foreach (var cell in targetCells)
                cell.Item = item;

            _itemToCellsDict.Add(item, targetCells);
            _bagItems.Add(item);
            _freeItems.Remove(item);
        }

        public void PutOutItem(Item item)
        {
            if (item == null)
                return;

            if (!_itemToCellsDict.ContainsKey(item))
                return;

            var cells = _itemToCellsDict[item];
            foreach (var cell in cells)
                cell.Item = null;

            _itemToCellsDict.Remove(item);
            _bagItems.Remove(item);
            _freeItems.Add(item);
        }

        public void SetupFreeItems(List<Item> freeItems)
        {
            _freeItems.Clear();
            _freeItems = freeItems;
        }

        public void RemoveItem(Item item)
        {
            PutOutItem(item);
            _bagItems.Remove(item);
            _freeItems.Remove(item);
        }

        public void Clear()
        {
            _bagItems.Clear();
            _freeItems.Clear();
            _itemToCellsDict.Clear();
            _cells.Clear();
        }
    }
}
