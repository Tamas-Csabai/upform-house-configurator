using System;
using UnityEngine;
using Upform.Graphs;
using Upform.Interaction;

namespace Upform.Designer
{
    public class DesignerHandler : MonoBehaviour
    {

        [Header("Parameters")]
        [SerializeField] private float defaultGridCellSize = 0.2f;
        [SerializeField] private float defaultAngleStep = 22.5f;
        [SerializeField] private float verticalOffset = 0.1f;

        [Header("References")]
        [SerializeField] private Graph graph;
        [SerializeField] private GridSnap gridSnap;
        [SerializeField] private AngleSnap angleSnap;

        [Header("Visuals")]
        [SerializeField] private AngleVisual angleVisual;
        [SerializeField] private LineVisual pointerLineVisual;

        [Header("Components")]
        [SerializeField] private DesignerComponenet[] designerComponenets;

        public float VerticalOffset => verticalOffset;
        public Graph Graph => graph;
        public GridSnap GridSnap => gridSnap;
        public AngleSnap AngleSnap => angleSnap;

        private void Start()
        {
            angleVisual.gameObject.SetActive(false);
            pointerLineVisual.gameObject.SetActive(false);

            gridSnap.Size = defaultGridCellSize;

            angleSnap.Step = defaultAngleStep;

            for (int i = 0; i < designerComponenets.Length; i++)
            {
                designerComponenets[i].Initialize(this);
            }
        }

        public void SetGridSize(float size)
        {
            gridSnap.Size = size;
        }

        public void SetGridSize(string sizeText)
        {
            SetGridSize(float.Parse(sizeText));
        }

        public void SetAngleStep(float step)
        {
            angleSnap.Step = step;
        }

        public void SetAngleStep(string stepText)
        {
            SetAngleStep(float.Parse(stepText));
        }

        public void SetAngleVisual(Vector3 crossPoint, Vector3 point1, Vector3 point2)
        {
            angleVisual.CrossPoint.position = crossPoint;
            angleVisual.Point1.position = point1;
            angleVisual.Point2.position = point2;
        }

        public void SetAngleVisualActive(bool isActive)
        {
            angleVisual.gameObject.SetActive(isActive);
        }

        public void SetPointerLineStartPosition(Vector3 startPosition)
        {
            pointerLineVisual.StartPoint.position = startPosition;
        }

        public void EnablePointerLineVisual()
        {
            InteractionManager.OnHovering += Hovering;

            pointerLineVisual.gameObject.SetActive(true);
        }

        public void DisablePointerLineVisual()
        {
            InteractionManager.OnHovering -= Hovering;

            pointerLineVisual.gameObject.SetActive(false);
        }

        private void Hovering(InteractionHit interactionHit)
        {
            pointerLineVisual.EndPoint.position = interactionHit.Point;
        }

#if UNITY_EDITOR
        [NaughtyAttributes.Button("Get " + nameof(designerComponenets))]
        private void EDITOR_GetDesignerComponenet()
        {
            EditorUtils.EDITOR_GetComponentsInChildren(this, ref designerComponenets);
        }
#endif

    }
}
