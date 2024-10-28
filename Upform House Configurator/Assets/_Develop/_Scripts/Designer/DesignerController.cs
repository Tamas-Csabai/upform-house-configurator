using UnityEngine;
using Upform.Graphs;

namespace Upform.Designer
{
    public class DesignerController : MonoBehaviour
    {

        [SerializeField] private float defaultGridCellSize = 0.5f;
        [SerializeField] private float defaultAngleStep = 45f;
        [SerializeField] private float verticalOffset = 0.1f;
        [SerializeField] private Graph graph;
        [SerializeField] private Grid grid;
        [SerializeField] private Angles angles;
        [SerializeField] private DesignerComponenet[] designerComponenets;

        private void Awake()
        {
            grid.Size = defaultGridCellSize;
            angles.Step = defaultAngleStep;

            for (int i = 0; i < designerComponenets.Length; i++)
            {
                designerComponenets[i].Initialize(verticalOffset, graph, grid, angles);
            }
        }

        public void SetGridSize(float size)
        {
            grid.Size = size;
        }

        public void SetGridSize(string sizeText)
        {
            SetGridSize(float.Parse(sizeText));
        }

        public void SetAngleStep(float step)
        {
            angles.Step = step;
        }

        public void SetAngleStep(string stepText)
        {
            SetAngleStep(float.Parse(stepText));
        }


#if UNITY_EDITOR
        [NaughtyAttributes.Button("Get " + nameof(designerComponenets))]
        private void EDITOR_GetDesignerComponenet()
        {
            EditorUtils.EDITOR_GetComponents(this, ref designerComponenets);
        }
#endif

    }
}
