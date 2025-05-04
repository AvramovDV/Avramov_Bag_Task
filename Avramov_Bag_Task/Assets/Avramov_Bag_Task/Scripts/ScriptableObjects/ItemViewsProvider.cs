using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Avramov.Bag
{
    [CreateAssetMenu(fileName = "ItemViewsProvider", menuName = "Avramov_Bag/ItemViewsProvider")]
    public class ItemViewsProvider : ScriptableObject
    {
        [SerializeField] private List<ItemSettings> _items;
        [SerializeField] private int _generationCount;
        [SerializeField] private int _generationLevel;
        [field: SerializeField] public int MaxLevel { get; private set; }

        public ItemView GetItem(ItemTypeEnum itemType, int level) => _items.Find(x => x.ItemType == itemType && x.Level == level)?.Prefab;

        public List<Item> GenerateItems()
        {
            var items = _items.Where(x => x.Level <= _generationLevel).ToList().GetRandomSubset(_generationCount);
            return items.Select(x => new Item(x.ItemType, x.Level)).ToList();
        }

        [Serializable]
        public class ItemSettings
        {
            [field: SerializeField] public ItemTypeEnum ItemType { get; private set; }
            [field: SerializeField] public int Level { get; private set; }
            [field: SerializeField] public ItemView Prefab { get; private set; }
        }
    }
}
