using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Avramov.Bag
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private CellColorsProvider _colorsProvider;

        [field: SerializeField] public RectTransform RectTransform { get; private set; }
        [field: SerializeField] public Vector2Int CellIndex { get; private set; }

        public void SetupIndex(Vector2Int index) => CellIndex = index;

        public void Setup(CellStateEnum cellState) => _image.color = _colorsProvider.GetColor(cellState);
    }
}
