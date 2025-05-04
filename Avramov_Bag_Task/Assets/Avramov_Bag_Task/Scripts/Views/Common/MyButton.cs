using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Avramov.Bag
{
    public class MyButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action ClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            ClickEvent?.Invoke();
        }
    }
}
