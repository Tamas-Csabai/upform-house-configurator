using UnityEngine;

namespace Upform.Interaction
{
    [RequireComponent(typeof(Collider))]
    public class InteractionTarget : MonoBehaviour
    {

        [SerializeField] private new Collider collider;
        [SerializeField] private Interactable interactable;

        public Interactable Interactable => interactable;

        private void Awake()
        {
            interactable.SetCollider(collider);
        }

    }
}
