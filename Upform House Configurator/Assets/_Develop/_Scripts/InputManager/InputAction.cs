
namespace Upform.Input
{
    public abstract class InputAction<T> : InputActionBase
    {

        public delegate T InputDelegate();

        protected InputDelegate _inputDelegate;
        protected T _value;

        public InputAction(InputDelegate inputDelegate) : base()
        {
            _inputDelegate = inputDelegate;
        }

        public override void Set(InputModifier inputModifier)
        {
            base.Set(inputModifier);

            if (_inputDelegate != null)
                _value = _inputDelegate.Invoke();
            else
                _value = default;
        }

        public virtual T Get()
        {
            return _value;
        }

    }
}
