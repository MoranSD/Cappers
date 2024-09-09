using System.Collections.Generic;

namespace Infrastructure.TickManagement
{
    public class TickManager
    {
        private List<ITickable> tickables;

        public TickManager()
        {
            tickables = new List<ITickable>();
        }

        public void Add(ITickable tickable)
        {
            if (tickables.Contains(tickable))
                throw new System.Exception(tickable.GetType().ToString());

            tickables.Add(tickable);
        }

        public void Remove(ITickable tickable)
        {
            if(!tickables.Contains(tickable))
                throw new System.Exception(tickable.GetType().ToString());

            tickables.Remove(tickable);
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < tickables.Count; i++)
                tickables[i].Update(deltaTime);
        }
    }
}
