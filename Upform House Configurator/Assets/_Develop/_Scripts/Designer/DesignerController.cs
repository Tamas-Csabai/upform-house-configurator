using UnityEngine;
using Upform.Graphs;

namespace Upform.Designer
{
    public class DesignerController : MonoBehaviour
    {

        [SerializeField] private float defaultGridCellSize = 0.5f;
        [SerializeField] private float verticalOffset = 0.1f;
        [SerializeField] private Graph graph;
        [SerializeField] private Grid grid;
        [SerializeField] private DesignerComponenet[] designerComponenets;

        private void Awake()
        {
            grid.Size = defaultGridCellSize;

            for (int i = 0; i < designerComponenets.Length; i++)
            {
                designerComponenets[i].Initialize(verticalOffset, graph, grid);
            }
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
