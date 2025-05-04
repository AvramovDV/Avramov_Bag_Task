using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Avramov.Bag
{
    public class MessageEffect : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private BaseTweenAnimation[] _animations;

        public void ShowText(string text)
        {
            _text.text = text;
            var sequence = DOTween.Sequence();

            foreach (var animation in _animations)
                sequence.Join(animation.Animate());

            sequence.OnComplete(() => Destroy(gameObject));
        }
    }
}
