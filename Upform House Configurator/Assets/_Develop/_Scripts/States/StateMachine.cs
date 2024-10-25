
using System.Collections.Generic;
using UnityEngine;

namespace Upform.States
{
    public class StateMachine : MonoBehaviour
    {

        [SerializeField] private StateSO defaultStateSO;
        [SerializeField] private StateBase[] states;

        private Empty_State _emptyState;
        private Dictionary<StateSO, StateBase> _statesBySO = new();
        private StateBase _currentState;

        public StateBase CurrentState => _currentState;

        private void Awake()
        {
            _emptyState = gameObject.AddComponent<Empty_State>();
            _currentState = _emptyState;

            for (int i = 0; i < states.Length; i++)
            {
                _statesBySO.Add(states[i].StateSO, states[i]);
                states[i].OnExit += ExitState;
            }
        }

        public void EnterDefaultState()
        {
            EnterState(defaultStateSO);
        }

        private void EnterState(StateSO stateSO)
        {
            _currentState = _statesBySO[stateSO];

            _currentState.OnEntering();
        }

        private void ExitState(StateSO nextStateSO)
        {
            SwitchState(nextStateSO);
        }

        public void SwitchState(StateSO stateSO)
        {
            if(_currentState != null)
            {
                _currentState.OnExiting();
            }

            EnterState(stateSO);
        }

#if UNITY_EDITOR
        [NaughtyAttributes.Button("Get " + nameof(states))]
        private void EDITOR_GetStates()
        {
            EditorUtils.EDITOR_GetComponents(this, ref states);
        }
#endif

    }
}
