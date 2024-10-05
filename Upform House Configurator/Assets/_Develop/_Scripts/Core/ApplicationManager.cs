using Upform.Input;
using Upform.Interaction;
using UnityEngine;

namespace Upform.Core
{
    public class ApplicationManager : MonoBehaviour
    {

        private void Start()
        {
            InteractionManager.StartAllInteractor();
        }

        private void Update()
        {
            InputManager.Poll();
        }

    }
}
