
using UnityEngine;

namespace Upform.Interaction
{
    public class CameraInteractor : Interactor
    {

        [SerializeField] private new Camera camera;
        [SerializeField] private LayerMask hitMask;
        [SerializeField] private float rayDistance;
        [SerializeField] private float rayRadius;
        [SerializeField] private int hitCapacity;

        private RaycastHit[] _hitBuffer;
        private RaycastHit _closestHit;
        private int _hitCount;
        private Ray _ray;

        protected override void Awake()
        {
            base.Awake();

            _hitBuffer = new RaycastHit[hitCapacity];
        }

        protected override InteractionHit Interact()
        {
            _ray = camera.ScreenPointToRay(Input.InputManager.Actions.PointerPosition.Get());

            _hitCount = Physics.SphereCastNonAlloc(_ray, rayRadius, _hitBuffer, rayDistance, hitMask, QueryTriggerInteraction.Collide);

            if(_hitCount <= 0)
            {
                return InteractionHit.Empty;
            }

            _closestHit = _hitBuffer[0];
            float sqrClosestDistance = (_hitBuffer[0].point - _ray.origin).sqrMagnitude;

            for (int i = 1; i < _hitCount; i++)
            {
                float sqrDistance = (_hitBuffer[i].point - _ray.origin).sqrMagnitude;

                if (sqrDistance < sqrClosestDistance)
                {
                    sqrClosestDistance = sqrDistance;
                    _closestHit = _hitBuffer[i];
                }
            }

            InteractionHit interactionHit = new()
            {
                HasHit = true,
                Interactable = null,
                Point = _closestHit.point,
                Normal = _closestHit.normal
            };

            if (_closestHit.transform.TryGetComponent(out InteractionTarget interactionTarget))
            {
                interactionHit.Interactable = interactionTarget.Interactable;
            }

            return interactionHit;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Debugger.ShowGizmos)
                return;

            Color gizmosColorBuffer = Gizmos.color;

            Gizmos.color = Color.yellow;

            Vector3 rayEndPoint;

            if(_hitCount > 0)
            {
                rayEndPoint = _closestHit.point;
            }
            else
            {
                rayEndPoint = _ray.origin + (rayDistance * _ray.direction);
            }

            Gizmos.DrawLine(_ray.origin, rayEndPoint);
            Gizmos.DrawWireSphere(rayEndPoint, rayRadius);

            Gizmos.color = gizmosColorBuffer;
        }
#endif

    }
}
