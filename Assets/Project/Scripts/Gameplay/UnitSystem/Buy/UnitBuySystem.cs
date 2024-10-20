using Gameplay.Ship.UnitControl.Placement;
using Gameplay.UnitSystem.Buy.Data;
using Gameplay.UnitSystem.Data;
using Gameplay.UnitSystem.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.UnitSystem.BuyMenu
{
    public class UnitBuySystem
    {
        private readonly ShipUnitPlacement unitPlacement;
        private readonly IUnitFactory unitFactory;

        private List<UnitToBuyData> unitsInStock;

        public UnitBuySystem(ShipUnitPlacement unitPlacement, IUnitFactory unitFactory)
        {
            this.unitPlacement = unitPlacement;
            this.unitFactory = unitFactory;
            unitsInStock = new List<UnitToBuyData>();
        }

        public void Initialize()
        {
            for (int i = 0; i < 3; i++)
                unitsInStock.Add(new UnitToBuyData()
                {
                    Id = i,
                    Price = new Random().Next(0, 10),
                    BodyType = (UnitBodyType)new Random().Next(0, 2),
                    Health = new Random().Next(0, 10),
                    Speed = new Random().Next(0, 10),
                    Damage = new Random().Next(0, 10),
                });
        }

        public UnitToBuyData[] GetUnitsInStock() => unitsInStock.ToArray();

        public bool TryBuyUnit(int unitId)
        {
            if (unitsInStock.Any(x => x.Id == unitId) == false)
                throw new Exception(unitId.ToString());

            if(unitPlacement.HasPlaceForUnit() == false)
                return false;

            //проверить, есть ли деньги в кошельке

            var unitToBuyData = unitsInStock.First(x => x.Id == unitId);

            //взять деньги из кошелька

            unitsInStock.Remove(unitToBuyData);

            var nextUnitId = unitPlacement.GetNextUnitId();
            var unitData = unitToBuyData.ToUnitData(nextUnitId);
            var unitController = unitFactory.Create(unitData);

            unitPlacement.AddUnit(unitController);

            return true;
        }
    }
}
