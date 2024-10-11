using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Upform.States
{
    public class StateMachine : MonoBehaviour
    {

        public enum EvaluationType
        {
            None,
            TickInterval,
            TimeInterval
        }

        [SerializeField] private StateSO defaultStateSO;
        [SerializeField] private StateBase[] states;

        private EmptyState _emptyState;
        private Dictionary<StateSO, StateBase> _statesBySO = new();
        private Coroutine _evaluationRoutine;
        private StateBase _currentState;

        public bool IsEvaluating { get; private set; } = false;

        public StateBase CurrentState => _currentState;

        private void Awake()
        {
            _emptyState = gameObject.AddComponent<EmptyState>();
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

        public void StartEvaluation()
        {
            IsEvaluating = true;

            if (_evaluationRoutine != null)
            {
                StopCoroutine(_evaluationRoutine);
            }

            if(_currentState.EvaluationType != EvaluationType.None)
            {
                if (_currentState.EvaluationRate == 0f)
                {
                    _evaluationRoutine = StartCoroutine(UpdateEvaluation());
                }
                else
                {
                    switch (_currentState.EvaluationType)
                    {
                        case EvaluationType.TickInterval:
                            _evaluationRoutine = StartCoroutine(TickEvaluation());
                            break;
                        case EvaluationType.TimeInterval:
                            _evaluationRoutine = StartCoroutine(TimeEvaluation());
                            break;
                    }
                }
            }
        }

        public void StopEvaluating()
        {
            IsEvaluating = false;

            if (_evaluationRoutine != null)
            {
                StopCoroutine(_evaluationRoutine);
            }

            _evaluationRoutine = null;
        }

        private IEnumerator TickEvaluation()
        {
            int tickCount = 0;

            while (enabled)
            {
                if(tickCount < _currentState.EvaluationRate)
                {
                    tickCount++;

                    yield return null;
                }
                else
                {
                    tickCount = 0;

                    _currentState.Evaluate();

                    yield return null;
                }
            }
        }

        private IEnumerator TimeEvaluation()
        {
            float time = 0f;

            while (enabled)
            {
                if(time < _currentState.EvaluationRate)
                {
                    time += Time.DeltaTime;

                    yield return null;
                }
                else
                {
                    time = 0f;

                    _currentState.Evaluate();

                    yield return null;
                }
            }
        }

        private IEnumerator UpdateEvaluation()
        {
            while (enabled)
            {
                _currentState.Evaluate();

                yield return null;
            }
        }

        public void SwitchState(StateSO stateSO)
        {
            if(_currentState != null)
            {
                _currentState.OnExiting();
            }

            EnterState(stateSO);

            StartEvaluation();
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
