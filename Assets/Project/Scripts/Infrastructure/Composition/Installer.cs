using UnityEngine;

namespace Infrastructure.Composition
{
    public abstract class Installer : MonoBehaviour
    {
        public virtual void Initialize() { }
        public virtual void Dispose() { }
    }
}