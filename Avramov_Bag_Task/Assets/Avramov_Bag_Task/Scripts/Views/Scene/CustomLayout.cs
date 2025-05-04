using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Avramov.Bag
{
    public class CustomLayout : MonoBehaviour
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private float _duration;

        public void PlaceItems(List<RectTransform> items, bool withAnimation)
        {
            items = items.OrderBy(x => x.position.x).ToList();
            List<Vector3> positions = new List<Vector3>(items.Count);
            float currentX = 0f;
            float targetWidth = items.Sum(x => x.rect.width);
            _transform.sizeDelta = new Vector2(targetWidth, _transform.sizeDelta.y);

            for (int i = 0; i < items.Count; i++)
            {
                float width = items[i].rect.width;
                Vector3 pos = new Vector3(currentX + width * 0.5f, 0f, 0f);
                positions.Add(pos);
                currentX += width;
                targetWidth += width;
                items[i].SetParent(_transform);
            }

            if(withAnimation)
            {
                for (int i = 0;i < positions.Count; i++)
                {
                    var item = items[i];
                    var pos = positions[i];
                    item.DOLocalMove(pos, _duration).OnComplete(() => PlaceItem(item, pos));
                }
            }
            else
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    var item = items[i];
                    var pos = positions[i];
                    PlaceItem(item, pos);
                }
            }
        }

        public void PlaceItem(Transform item, Vector3 position) => item.localPosition = position;
    }
}
