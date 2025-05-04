using System.Collections.Generic;
using UnityEngine;

namespace Avramov.Bag
{
    [CreateAssetMenu(fileName = "BagsProvider", menuName = "Avramov_Bag/BagsProvider")]
    public class BagsProvider : ScriptableObject
    {
        [SerializeField] private List<BagView> _bags;

        public IReadOnlyList<BagView> Bags => _bags;
    }
}
