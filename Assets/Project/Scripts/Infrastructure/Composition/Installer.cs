using UnityEngine;

namespace Infrastructure.Composition
{
    public abstract class Installer : MonoBehaviour
    {
        public virtual void PostInitialize() { }
        public virtual void Initialize() { }
        public virtual void LateInitialize() { }
        public virtual void Dispose() { }
    }
}