using UnityEngine;

namespace Upform.States
{
    public abstract class StateBase : MonoBehaviour
    {

        [SerializeField] private StateSO stateSO;

        public event System.Action<StateSO> OnExit;

        public StateSO StateSO => stateSO;

        public abstract void OnEntering();

        public abstract void OnExiting();

        public void Exit(StateSO nextStateSO)
        {
            OnExit?.Invoke(nextStateSO);
        }

    }
}
