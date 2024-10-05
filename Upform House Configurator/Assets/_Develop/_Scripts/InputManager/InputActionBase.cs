
namespace Upform.Input
{
    public abstract class InputActionBase
    {

        public InputModifier Modifiers { get; protected set; }

        public InputActionBase()
        {
            InputManager.SubscribeAction(this);
        }

        ~InputActionBase()
        {
            InputManager.UnsubscribeAction(this);
        }

        public virtual void Set(InputModifier inputModifier)
        {
            Modifiers = inputModifier;
        }

    }
}
