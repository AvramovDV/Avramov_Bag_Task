using UnityEngine;

namespace Avramov.Bag
{
    public class GameControlScreen : MonoBehaviour
    {
        [field: SerializeField] public MyButton BackButton { get; private set; }
        [field: SerializeField] public MyButton PlayButton { get; private set; }
    }
}
