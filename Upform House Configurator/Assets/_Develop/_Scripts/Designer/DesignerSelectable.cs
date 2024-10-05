using UnityEngine;
using Upform.Selection;

namespace Upform.Designer
{
    public class DesignerSelectable : Selectable
    {

        [SerializeField] private DesignerEntity designerEntity;

        public DesignerEntity DesignerEntity => designerEntity;

    }
}
