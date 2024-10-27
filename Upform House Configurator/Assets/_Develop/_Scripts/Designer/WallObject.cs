using UnityEngine;
using Upform.Core;

namespace Upform.Designer
{
    public class WallObject : EntityComponent
    {

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Collider collider;

        private Wall _wall;
        private WallObjectSO _wallObjectSO;

        public Wall Wall
        {
            get => _wall;
            set
            {
                if (value != null)
                {
                    _wall = value;
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

        public void SetCollider(bool enabled)
        {
            collider.enabled = enabled;
        }

    }
}
