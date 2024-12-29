using Gameplay.Game;
using System.Linq;
using Utils;

namespace Gameplay.UnitSystem.Upgrade
{
    public class UnitUpgradeSystem
    {
        private readonly GameState gameState;

        public UnitUpgradeSystem(GameState gameState)
        {
            this.gameState = gameState;
        }

        public bool TryHealUnit(int unitId)
        {
            if (gameState.HasUnit(unitId) == false)
                throw new System.Exception(unitId.ToString());

            var data = gameState.GetUnitDataById(unitId);

            if(data.CurrentHealth == data.MaxHealth)
                throw new System.Exception(unitId.ToString());

            //todo:

            //проверить, есть ли деньги в кошельке

            data.CurrentHealth = data.MaxHealth;
            gameState.ReplaceUnitDataById(unitId, data);

            EventBus.Invoke<UnitHealedEvent>(new()
            {
                UnitId = unitId,
            });

            //взять деньги из кошелька

            return true;
        }

        public bool TryUpgradeUnit(int unitId)
        {
            if (gameState.HasUnit(unitId) == false)
                throw new System.Exception(unitId.ToString());

            var data = gameState.GetUnitDataById(unitId);

            if (data.CurrentHealth != data.MaxHealth)
                throw new System.Exception(unitId.ToString());

            if (data.UpgradeLevel == 3)
                return false;

            //todo:

            //проверить, есть ли деньги в кошельке

            data.UpgradeLevel++;

            switch (data.BodyType)
            {
                case Data.UnitBodyType.small:
                    data.Damage++;
                    break;
                case Data.UnitBodyType.medium:
                    data.Speed++;
                    break;
                case Data.UnitBodyType.big:
                    data.CurrentHealth++;
                    data.MaxHealth++;
                    break;
            }

            gameState.ReplaceUnitDataById(unitId, data);

            EventBus.Invoke<UnitUpgradedEvent>(new()
            {
                UnitId = unitId,
            });
            //взять деньги из кошелька внутри switch ибо цена зависит от уровня прокачки

            return true;
        }
    }
}
