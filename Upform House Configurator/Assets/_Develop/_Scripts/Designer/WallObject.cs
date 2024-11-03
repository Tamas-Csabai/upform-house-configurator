
using UnityEngine;
using Upform.Core;

namespace Upform.Designer
{
    public class WallObject : EntityComponent
    {

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private new Collider collider;

        private Wall _wall;
        private WallObjectSO _wallObjectSO;
        private float _wallPositionInPercent;

        public Wall Wall
        {
            get => _wall;
            set
            {
                if(_wall != null)
                {
                    _wall.OnMeshChanged -= WallMeshChanged;
                }

                if (value != null)
                {
                    _wall = value;
                    _wall.OnMeshChanged += WallMeshChanged;
                    meshRenderer.transform.localScale = new Vector3(_wallObjectSO.Width, meshRenderer.transform.localScale.y, _wall.Thickness);
                }
                else
                {
                    _wall = null;
                    meshRenderer.transform.localScale = new Vector3(_wallObjectSO.Width, meshRenderer.transform.localScale.y, meshRenderer.transform.localScale.z);
                }
            }
        }

        public WallObjectSO WallObjectSO
        {
            get => _wallObjectSO;
            set
            {
                _wallObjectSO = value;

                if(_wall != null)
                {
                    meshRenderer.transform.localScale = new Vector3(_wallObjectSO.Width, meshRenderer.transform.localScale.y, _wall.Thickness);
                }
                else
                {
                    meshRenderer.transform.localScale = new Vector3(_wallObjectSO.Width, meshRenderer.transform.localScale.y, meshRenderer.transform.localScale.z);
                }
            }
        }

        private void OnDestroy()
        {
            if (_wall != null)
            {
                _wall.OnMeshChanged -= WallMeshChanged;
            }
        }

        private void WallMeshChanged()
        {
            Vector3 startNodePosition = _wall.Edge.StartNode.transform.position;
            Vector3 endNodePosition = _wall.Edge.EndNode.transform.position;

            float wallDistance = (endNodePosition - startNodePosition).magnitude;

            transform.position = startNodePosition + (_wallPositionInPercent * (wallDistance / 100f) * (endNodePosition - startNodePosition).normalized);
            transform.forward = _wall.Perpendicular;
        }

        public void Move(Vector3 position)
        {
            if(_wall != null)
            {
                transform.position = _wall.GetClosestPosition(position);
                transform.forward = _wall.Perpendicular;

                Vector3 startNodePosition = _wall.Edge.StartNode.transform.position;
                Vector3 endNodePosition = _wall.Edge.EndNode.transform.position;

                float wallDistance = (endNodePosition - startNodePosition).magnitude;
                float wallPosition = (transform.position - startNodePosition).magnitude;

                _wallPositionInPercent = 100f / (wallDistance / wallPosition);
            }
            else
            {
                transform.position = position;
                transform.forward = Vector3.forward;
            }
        }

        public void SetCollider(bool enabled)
        {
            collider.enabled = enabled;
        }

    }
}
