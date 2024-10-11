using UnityEngine;

namespace Upform.States
{
    public abstract class StateBase : MonoBehaviour
    {

        [SerializeField] private StateSO stateSO;
        [SerializeField] private StateMachine.EvaluationType evaluationType = StateMachine.EvaluationType.None;
        [SerializeField] private float evaluationRate = 0f;

        public event System.Action<StateSO> OnExit;

        public StateSO StateSO => stateSO;
        public StateMachine.EvaluationType EvaluationType => evaluationType;
        public float EvaluationRate => evaluationRate;

        public abstract void OnEntering();

        public abstract void Evaluate();

        public abstract void OnExiting();

        public void Exit(StateSO nextStateSO)
        {
            OnExit?.Invoke(nextStateSO);
        }

    }
}
