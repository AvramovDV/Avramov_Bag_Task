using UnityEngine;

namespace Avramov.Bag
{
    public class MessagesManager : MonoBehaviour
    {
        [SerializeField] private MessageEffect _prefab;

        public void ShowMessage(string message, Vector3 position)
        {
            MessageEffect effect = Instantiate(_prefab, position, Quaternion.identity, transform);
            effect.ShowText(message);
        }
    }
}
