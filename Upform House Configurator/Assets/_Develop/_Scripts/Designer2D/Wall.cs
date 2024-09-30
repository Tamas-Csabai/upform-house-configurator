using System.Drawing;
using UnityEngine;

namespace Upform
{
    public class Wall : MonoBehaviour
    {

        public Material material;
        public Transform[] points;

        private void Update()
        {
            Mesh quad = MeshBuilder.CreateQuad(points[0].position, points[1].position, points[2].position, points[3].position);

            RenderParams rp = new RenderParams(material);

            Graphics.RenderMesh(rp, quad, 0, Matrix4x4.Translate(transform.position));
        }

    }
}
