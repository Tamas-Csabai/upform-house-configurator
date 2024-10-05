
namespace Upform.Input
{
    public class ButtonAction : InputAction<bool>
    {

        protected bool _isDown = false;
        protected bool _isUp = false;
        protected bool _wasDown = false;
        protected bool _wasUp = false;

        public event System.Action<InputModifier> OnDown;
        public event System.Action<InputModifier> OnUp;

        public ButtonAction(InputDelegate inputDelegate) : base(inputDelegate) { }

        public override void Set(InputModifier inputModifier)
        {
            base.Set(inputModifier);

            if (_value)
            {
                if (_wasUp)
                {
                    _isUp = false;
                    _wasUp = false;
                }

                if (_wasDown)
                {
                    _isDown = false;
                }
                else
                {
                    _isDown = true;
                    _wasDown = true;

                    OnDown?.Invoke(inputModifier);
                }
            }
            else
            {
                if(_wasDown)
                {
                    _isDown = false;
                    _wasDown = false;
                }

                if (_wasUp)
                {
                    _isUp = false;
                }
                else
                {
                    _isUp = true;
                    _wasUp = true;

                    OnUp?.Invoke(inputModifier);
                }
            }
        }

        public virtual bool GetDown()
        {
            return _isDown;
        }

        public virtual bool GetUp()
        {
            return _isUp;
        }
        
    }
}
