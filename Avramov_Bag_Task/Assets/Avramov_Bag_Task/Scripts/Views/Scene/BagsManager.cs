using DG.Tweening;
using UnityEngine;

namespace Avramov.Bag
{
    public class BagsManager : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _destroyPoint;
        [SerializeField] private Transform _bagPoint;
        [SerializeField] private float _duration;

        public BagView Bag { get; private set; }

        public void ShowBag(BagView bag, bool isNext, bool withAnimation)
        {
            if (Bag != null)
                DestroyBag(Bag, isNext, withAnimation);


            Vector3 position = isNext ? _spawnPoint.position : _destroyPoint.position;
            Bag = Instantiate(bag, position, Quaternion.identity, transform);

            if (!withAnimation)
                SetBagPlace();
            else
                Bag.transform.DOMove(_bagPoint.position, _duration).OnComplete(() => SetBagPlace());
        }

        private void DestroyBag(BagView bag, bool isNext, bool withAnimation)
        {
            if (withAnimation)
            {
                Vector3 position = !isNext ? _spawnPoint.position : _destroyPoint.position;
                bag.transform.DOMove(position, _duration).OnComplete(() => Destroy(bag.gameObject));
                Bag = null;
            }
            else
                Destroy(bag.gameObject);
        }

        private void SetBagPlace() => Bag.transform.position = _bagPoint.position;
    }
}
