using DG.Tweening;
using UnityEngine;

namespace Avramov.Bag
{
    public class FadeTweenAnimation : BaseTweenAnimation
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _start;
        [SerializeField] private float _end;
        [SerializeField] private float _duration;

        public override Tween Animate()
        {
            _canvasGroup.alpha = _start;
            return _canvasGroup.DOFade(_end, _duration);
        }
    }
}
