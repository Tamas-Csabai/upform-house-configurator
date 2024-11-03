
using System;
using UnityEngine;
using Upform.Graphs;
using Upform.Interaction;
using Upform.Selection;
using Upform.States;

namespace Upform.Designer
{
    public class CreateNewWalls_State : StateBase
    {

        [SerializeField] private StateSO emptySelectionStateSO;
        [SerializeField] private WallSO wallSO;
        [SerializeField] private Graph graph;
        [SerializeField] private IntersectionCreator intersectionCreator;

        private Intersection _startIntersection;
        private Intersection _endIntersection;
        private Wall _newWall;

        public override void OnEntering()
        {
            SelectionManager.ClearSelection();

            _startIntersection = null;
            _endIntersection = null;
            _newWall = null;

            InteractionManager.OnAction += Cancel;

            intersectionCreator.OnNewIntersection += NewIntersection;
            intersectionCreator.OnIntersectionSelected += IntersectionSelected;
            intersectionCreator.OnNewIntersectionOnWall += NewIntersectionOnWall;
            intersectionCreator.StartInteraction(wallSO);
        }

        public override void OnExiting()
        {
            InteractionManager.OnAction -= Cancel;

            intersectionCreator.StopInteraction();
            intersectionCreator.OnNewIntersection -= NewIntersection;
            intersectionCreator.OnIntersectionSelected -= IntersectionSelected;
            intersectionCreator.OnNewIntersectionOnWall -= NewIntersectionOnWall;
        }

        private void NewIntersection(Intersection intersection)
        {
            if (_startIntersection == null)
            {
                _startIntersection = intersection;
            }
            else
            {
                ConnectWall(intersection);
            }
        }

        private void IntersectionSelected(Intersection intersection)
        {
            if (_startIntersection == null)
            {
                _startIntersection = intersection;
            }
            else
            {
                if (_startIntersection != intersection)
                {
                    ConnectWall(intersection);
                }

                Confirm();
            }
        }

        private void NewIntersectionOnWall(Intersection intersection, Wall wall)
        {
            if (_startIntersection == null)
            {
                _startIntersection = intersection;
            }
            else
            {
                ConnectWall(intersection);

                Confirm();
            }
        }

        private void ConnectWall(Intersection intersection)
        {
            _endIntersection = intersection;

            Edge newEdge = graph.ConnectNodes(_startIntersection.Node, _endIntersection.Node);

            _newWall = newEdge.GetComponent<Wall>();
            _newWall.WallSO = wallSO;

            _startIntersection = _endIntersection;
        }

        private void Confirm()
        {
            SelectionManager.ClearSelection();

            Exit(emptySelectionStateSO);
        }

        private void Cancel(InteractionHit interactionHit)
        {
            Confirm();
        }

    }
}
