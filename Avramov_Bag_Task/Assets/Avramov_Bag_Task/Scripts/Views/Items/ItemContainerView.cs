using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Avramov.Bag
{
    public class ItemContainerView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private ItemViewsProvider _itemsProvider;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _left;
        [SerializeField] private Transform _right;
        [SerializeField] private float _duration;

        [field: SerializeField] public RectTransform RectTransform { get; private set; }

        private ItemView _view;
        private Tween _animationTween;

        public Item Item { get; private set; }

        public IReadOnlyList<Transform> CellPoints => _view.CellPoints;

        public event Action<ItemContainerView, PointerEventData> BeginDragEvent;
        public event Action<ItemContainerView, PointerEventData> DragEvent;
        public event Action<ItemContainerView, PointerEventData> EndDragEvent;
        public event Action<ItemContainerView, PointerEventData> DropEvent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent?.Invoke(this, eventData);
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragEvent?.Invoke(this, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent?.Invoke(this, eventData);
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            DropEvent?.Invoke(this, eventData);
        }

        public void ShowItem(Item item)
        {
            Item = item;
            ShowItem(item.Type, item.Level);
        }

        public void MoveToBag(Vector3 firstCellPointPosition)
        {
            Vector3 offset = transform.position - CellPoints[0].position;
            Vector3 target = firstCellPointPosition + offset;
            transform.position = target;
        }

        public void SetAnimation(bool isOn)
        {
            Debug.Log($"SetAnimation: {isOn}; {_animationTween.IsActive()}");
            if (isOn)
            {
                if (_animationTween.IsActive())
                    return;

                _animationTween = DOTween.Sequence()
                    .Append(transform.DORotate(_left.eulerAngles, _duration))
                    .Append(transform.DORotate(_right.eulerAngles, _duration))
                    .Append(transform.DORotate(Vector3.zero, _duration))
                    .SetLoops(-1);
            }
            else
            {
                _animationTween.Kill();
                _animationTween = null;
                transform.rotation = Quaternion.identity;
            }
        }

        private void ShowItem(ItemTypeEnum type, int level)
        {
            if (_view != null)
                Destroy(_view.gameObject);

            ItemView prefab = _itemsProvider.GetItem(type, level);
            _view = Instantiate(prefab, transform);
            RectTransform.sizeDelta = _view.RectTransform.sizeDelta;
        }
    }
}
