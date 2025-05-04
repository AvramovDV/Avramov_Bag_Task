using System.Collections.Generic;
using UnityEngine;

namespace Avramov.Bag
{
    public static class ListExtentions
    {
        public static List<T> GetMixed<T>(this List<T> list) => list.GetRandomSubset(list.Count);

        public static List<T> GetRandomSubset<T>(this List<T> list, int count)
        {
            List<T> subset = new List<T>(list.Count);
            subset.AddRange(list);

            int range = subset.Count;

            for (int i = 0; i < subset.Count; i++)
            {
                int index = Random.Range(0, range);
                T item = subset[index];
                subset[index] = subset[subset.Count - 1];
                subset[subset.Count - 1] = item;
                range--;
            }

            return subset.GetRange(0, count);
        }
    }
}
