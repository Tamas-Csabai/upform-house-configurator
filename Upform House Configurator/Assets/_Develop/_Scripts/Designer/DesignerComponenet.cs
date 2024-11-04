using UnityEngine;

namespace Upform.Designer
{
    public abstract class DesignerComponenet : MonoBehaviour
    {

        protected DesignerHandler _designerHandler;

        public void Initialize(DesignerHandler designerHandler)
        {
            _designerHandler = designerHandler;
        }

    }
}
