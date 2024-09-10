using UnityEngine;

namespace Infrastructure.Composition
{
    public class CompositionController : ICompositionController
    {
        public void Dispose()
        {
            var compositionRoot = Object.FindObjectOfType<CompositionRoot>();

            if(compositionRoot != null) 
                compositionRoot.Dispose();
        }

        public void Initialize()
        {
            var compositionRoot = Object.FindObjectOfType<CompositionRoot>();

            if (compositionRoot != null)
                compositionRoot.Initialize();
        }
    }
}