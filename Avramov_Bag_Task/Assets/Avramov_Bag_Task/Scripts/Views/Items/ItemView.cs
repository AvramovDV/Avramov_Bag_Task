using System.Collections.Generic;
using UnityEngine;

namespace Avramov.Bag
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private List<Transform> _cellPoints;
        [field: SerializeField] public RectTransform RectTransform { get; private set; }

        public IReadOnlyList<Transform> CellPoints => _cellPoints;

    }
}
