using DG.Tweening;
using UnityEngine;

namespace Avramov.Bag
{
    public class MoveTweenAnimation : BaseTweenAnimation
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private float _duration;

        public override Tween Animate()
        {
            _target.position = _start.position;
            return _target.DOMove(_end.position, _duration);
        }
    }
}
