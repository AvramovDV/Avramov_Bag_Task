using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Avramov.Bag
{
    public class ItemsManager : MonoBehaviour
    {
        [field: SerializeField] public CustomLayout FreeItemsLayout { get; private set; }
        [SerializeField] public ItemContainerView _itemPrefab;

        private List<ItemContainerView> _items = new List<ItemContainerView>();

        public event Action<ItemContainerView, PointerEventData> BeginDragEvent;
        public event Action<ItemContainerView, PointerEventData> DragEvent;
        public event Action<ItemContainerView, PointerEventData> EndDragEvent;
        public event Action<ItemContainerView, PointerEventData> DropEvent;

        public List<ItemContainerView> CreateItems(List<Item> items)
        {
            List<ItemContainerView> result = new List<ItemContainerView>();
            foreach (Item item in items)
            {
                var view = CreateItem(item);
                result.Add(view);
            }
            return result;
        }

        public ItemContainerView CreateItem(Item item)
        {
            ItemContainerView view = Instantiate(_itemPrefab, transform);
            view.ShowItem(item);
            view.BeginDragEvent += BeginDrag;
            view.DragEvent += Drag;
            view.EndDragEvent += EndDrag;
            view.DropEvent += Drop;
            _items.Add(view);
            return view;
        }

        public ItemContainerView GetView(Item item) => _items.Find(x => x.Item == item);

        public void DestroyItems(List<Item> items)
        {
            foreach (Item item in items)
                DestroyItem(item);
        }

        public void DestroyItem(Item item)
        {
            var view = GetView(item);
            _items.Remove(view);
            DestroyItem(view);
        }

        public void DestroyItem(ItemContainerView view)
        {
            view.BeginDragEvent -= BeginDrag;
            view.DragEvent -= Drag;
            view.EndDragEvent -= EndDrag;
            view.DropEvent -= Drop;
            Destroy(view.gameObject);
        }

        public void StopAnimation()
        {
            foreach(var item in _items)
                item.SetAnimation(false);
        }

        public void Clear()
        {
            foreach (var item in _items)
            {
                DestroyItem(item);
            }

            _items.Clear();
        }

        private void BeginDrag(ItemContainerView view, PointerEventData eventData) => BeginDragEvent?.Invoke(view, eventData);
        private void Drag(ItemContainerView view, PointerEventData eventData) => DragEvent?.Invoke(view, eventData);
        private void EndDrag(ItemContainerView view, PointerEventData eventData) => EndDragEvent?.Invoke(view, eventData);
        private void Drop(ItemContainerView view, PointerEventData eventData) => DropEvent?.Invoke(view, eventData);
    }
}
