using UnityEngine;
using Upform.Interaction;

namespace Upform.Designer
{
    public class WallObjectCreator : DesignerComponenet
    {

        [SerializeField] private WallObject wallObjectPrefab;
        [SerializeField] private Transform wallObjectsParent;

        private WallObject _newWallObject;
        private Wall _currentHoveredWall;
        private WallObjectSO _wallObjectSO;

        public event System.Action<WallObject> OnNewWallObject;

        public void StartInteraction(WallObjectSO wallObjectSO)
        {
            _wallObjectSO = wallObjectSO;

            _newWallObject = null;
            _currentHoveredWall = null;

            InteractionManager.OnInteract += Interact;
            InteractionManager.OnHovering += Hovering;
            InteractionManager.OnHoverEnter += HoverEnter;
            InteractionManager.OnHoverExit += HoverExit;

            _newWallObject = CreateNewWallObject();
        }

        public void StopInteraction()
        {
            InteractionManager.OnInteract -= Interact;
            InteractionManager.OnHovering -= Hovering;
            InteractionManager.OnHoverEnter -= HoverEnter;
            InteractionManager.OnHoverExit -= HoverExit;

            if(_newWallObject != null)
            {
                Destroy(_newWallObject.gameObject);
            }
        }

        private void Interact(InteractionHit interactionHit)
        {
            if(_currentHoveredWall != null)
            {
                _newWallObject.SetCollider(true);

                WallObject placedWallObject = _newWallObject;

                _newWallObject = CreateNewWallObject();

                OnNewWallObject?.Invoke(placedWallObject);
            }
        }

        private void Hovering(InteractionHit interactionHit)
        {
            Vector3 newWallObjectPosition = interactionHit.Point;

            if (_currentHoveredWall != null)
            {
                newWallObjectPosition = _currentHoveredWall.GetClosestPosition(interactionHit.Point);
            }

            _newWallObject.Move(newWallObjectPosition);
        }

        private void HoverEnter(InteractionHit interactionHit)
        {
            if (interactionHit.Interactable.HasLayer(InteractionLayer.Wall))
            {
                _currentHoveredWall = interactionHit.Interactable.GetComponent<Wall>();

                _newWallObject.Wall = _currentHoveredWall;
            }
        }

        private void HoverExit(InteractionHit interactionHit)
        {
            _currentHoveredWall = null;

            _newWallObject.Wall = null;
        }

        private WallObject CreateNewWallObject()
        {
            WallObject newWallObject = Instantiate(wallObjectPrefab);
            newWallObject.transform.SetParent(wallObjectsParent, true);
            newWallObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            newWallObject.WallObjectSO = _wallObjectSO;
            newWallObject.SetCollider(false);

            return newWallObject;
        }
    }
}
