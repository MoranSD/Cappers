using UnityEngine;

namespace Infrastructure.Composition
{
    public class CompositionRoot : MonoBehaviour
    {
        private Installer[] initializers;
        private bool isInitialized = false;

        public void Initialize()
        {
            if(isInitialized) return;
            isInitialized = true;

            initializers = GetComponentsInChildren<Installer>();

            foreach (var initializer in initializers)
                initializer.Initialize();
        }

        public void Dispose()
        {
            if (isInitialized == false) return;
            isInitialized = false;

            foreach (var initializer in initializers)
                initializer.Dispose();
        }
    }
}