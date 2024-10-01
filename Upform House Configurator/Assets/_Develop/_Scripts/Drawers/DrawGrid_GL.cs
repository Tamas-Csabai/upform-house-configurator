using UnityEngine;
using UnityEngine.Rendering;

namespace Upform
{
    public class DrawGrid_GL : MonoBehaviour
    {

        [SerializeField] private Camera renderCamera;
        [SerializeField] private Material lineMaterial;
        [SerializeField] private float cellSize = 1.0f;
        [SerializeField] private int gridSize = 10;

        private bool _canRender = false;

        private void Awake()
        {
            RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
            RenderPipelineManager.endCameraRendering += EndCameraRendering;
        }

        private void OnDestroy()
        {
            RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
            RenderPipelineManager.endCameraRendering -= EndCameraRendering;
        }

        private void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (camera == renderCamera)
                _canRender = true;
        }

        private void EndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (camera == renderCamera)
                _canRender = false;
        }

        private void OnRenderObject()
        {
            if (_canRender)
            {
                _canRender = false;
                DrawGrid();
            }
        }

        private void DrawGrid()
        {
            GL.PushMatrix();

            lineMaterial.SetPass(0);
            GL.Color(lineMaterial.color);

            GL.Begin(GL.LINES);

            for (int i = 0; i < gridSize + 1; i++)
            {
                GL.Vertex3((transform.position.x - (gridSize / 2f)) * cellSize, transform.position.y, (transform.position.z - (gridSize / 2f) + i) * cellSize);
                GL.Vertex3((transform.position.x + (gridSize / 2f)) * cellSize, transform.position.y, (transform.position.z - (gridSize / 2f) + i) * cellSize);

                GL.Vertex3((transform.position.x - (gridSize / 2f) + i) * cellSize, transform.position.y, (transform.position.z - (gridSize / 2f)) * cellSize);
                GL.Vertex3((transform.position.x - (gridSize / 2f) + i) * cellSize, transform.position.y, (transform.position.z + (gridSize / 2f)) * cellSize);
            }

            GL.End();

            GL.PopMatrix();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (lineMaterial != null)
                DrawGrid();
        }
#endif
    }
}
