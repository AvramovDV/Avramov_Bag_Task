using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Avramov.Bag
{
    public class BagView : MonoBehaviour
    {
        [SerializeField] private CellView[] _cells;
        [SerializeField] private float _step;
        [SerializeField] private Transform _cornerPoint;
        [SerializeField] private Transform _targetTest;

        [Button]
        private void InitCells()
        {
            _cells = GetComponentsInChildren<CellView>();
            foreach (CellView cell in _cells) 
                SetupIndex(cell);
        }

        public Vector2Int GetIndex(Vector3 position)
        {
            Vector2 pos = _cornerPoint.InverseTransformPoint(position);
            pos /= _step;
            Vector2Int index = new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
            return index;
        }

        public Vector3 GetPosition(Vector2Int index)
        {
            Vector3 offset = new Vector3(index.x * _step, index.y * _step, 0f);
            return _cornerPoint.position + offset;
        }

        public List<CellData> GetCells()
        {
            return _cells.Select(x => new CellData() { Index = x.CellIndex }).ToList();
        }

        public void SetupCellsColors(List<CellData> cells, CellStateEnum cellStateEnum)
        {
            foreach (var cell in _cells)
            {
                if(cells.Any(x => x.Index == cell.CellIndex))
                {
                    cell.Setup(cellStateEnum);
                }
                else
                {
                    cell.Setup(CellStateEnum.None);
                }
            }
        }

        private void SetupIndex(CellView cell)
        {
            Vector2Int index = GetIndex(cell.transform.position);            
            cell.SetupIndex(index);
        }
    }
}
