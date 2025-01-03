using UnityEngine;

namespace Infrastructure.Composition
{
    public abstract class Installer : MonoBehaviour
    {
        public virtual void PreInitialize() { }
        public virtual void Initialize() { }
        public virtual void AfterInitialize() { }
        public virtual void Dispose() { }
    }
}