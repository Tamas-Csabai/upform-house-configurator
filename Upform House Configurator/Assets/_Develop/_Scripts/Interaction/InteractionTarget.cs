using UnityEngine;

namespace Upform.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class InteractionTarget : MonoBehaviour
    {

        [SerializeField] private Interactable interactable;

        public Interactable Interactable => interactable;

    }
}
