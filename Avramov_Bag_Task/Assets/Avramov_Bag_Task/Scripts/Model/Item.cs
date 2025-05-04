using UnityEngine;

namespace Avramov.Bag
{
    public class Item
    {
        public ItemTypeEnum Type { get; private set; }
        public int Level { get; private set; }

        public Item(ItemTypeEnum type, int level)
        {
            Type = type;
            Level = level;
        }

        public void LevelUp()
        {
            Level++;
        }
    }
}
