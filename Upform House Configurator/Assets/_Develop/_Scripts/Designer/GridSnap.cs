using UnityEngine;

namespace Upform.Designer
{
    public class GridSnap : MonoBehaviour
    {

        private float _size = 0f;

        public float Size
        {
            get => _size;
            set
            {
                _size = value;
            }
        }

        public Vector3 WorldToCellOnPlane(Vector3 worldPosition)
        {
            if(_size == 0f)
            {
                return worldPosition;
            }

            Vector3 cellPosition = worldPosition / _size;
            cellPosition.x = Mathf.FloorToInt(cellPosition.x) * _size;
            cellPosition.y = worldPosition.y;
            cellPosition.z = Mathf.FloorToInt(cellPosition.z) * _size;

            return cellPosition;
        }

    }
}
