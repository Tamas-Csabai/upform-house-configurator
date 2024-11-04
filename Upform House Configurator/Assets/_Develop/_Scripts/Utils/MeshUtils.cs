using UnityEngine;

namespace Upform
{
    public static class MeshUtils
    {

        public static Mesh CreateNewQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4]
            {
                a,
                b,
                c,
                d
            };

            mesh.vertices = vertices;

            int[] tris = new int[6]
            {
                0, 2, 1, // lower left triangle
                2, 3, 1 // upper right triangle
            };

            mesh.triangles = tris;

            Vector3[] normals = new Vector3[4]
            {
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward
            };

            mesh.normals = normals;

            Vector2[] uv = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };

            mesh.uv = uv;

            return mesh;
        }

        public static void SetQuadVertices(ref Mesh mesh, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            Vector3[] vertices = new Vector3[4]
            {
                a,
                b,
                c,
                d
            };

            mesh.vertices = vertices;
        }
    }
}
