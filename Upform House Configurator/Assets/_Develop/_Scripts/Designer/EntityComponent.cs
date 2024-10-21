using UnityEngine;

namespace Upform.Core
{
    public abstract class EntityComponent : MonoBehaviour
    {

        [SerializeField] protected Entity entity;

        public Entity Entity => entity;

        public void SetEntity(Entity entity)
        {
            this.entity = entity;
        }

    }
}
