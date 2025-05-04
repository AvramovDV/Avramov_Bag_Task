using TMPro;
using UnityEngine;

namespace Avramov.Bag
{
    public class ChooseBagScreen : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text BagsText { get; private set; }
        [field: SerializeField] public MyButton NextMyButton { get; private set; }
        [field: SerializeField] public MyButton PreviousMyButton { get; private set; }
        [field: SerializeField] public MyButton SelectMyButton { get; private set; }
        [field: SerializeField] public MyButton ExitMyButton { get; private set; }
    }
}
