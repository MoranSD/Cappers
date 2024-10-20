using Gameplay.UnitSystem.Data;

namespace Gameplay.UnitSystem.Controller
{
    public class UnitController
    {
        public UnitData Data => data;

        private UnitData data;

        public UnitController(UnitData data)
        {
            this.data = data;
        }
    }
}
