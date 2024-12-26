using Gameplay.Ship.UnitControl;
using Gameplay.UnitSystem.Buy.Data;
using Gameplay.UnitSystem.Buy.View;
using Gameplay.UnitSystem.Data;
using Gameplay.UnitSystem.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.UnitSystem.BuyMenu
{
    public class UnitBuySystem
    {
        private readonly ShipUnitExistenceControl unitExistenceControl;
        private readonly IUnitFactory unitFactory;
        private readonly IUnitBuySystemView view;
        private List<UnitToBuyData> unitsInStock;

        public UnitBuySystem(ShipUnitExistenceControl unitExistenceControl, IUnitFactory unitFactory, IUnitBuySystemView view)
        {
            this.unitExistenceControl = unitExistenceControl;
            this.unitFactory = unitFactory;
            this.view = view;
            unitsInStock = new List<UnitToBuyData>();
        }

        public void Initialize()
        {
            for (int i = 0; i < 3; i++)
                unitsInStock.Add(new UnitToBuyData()
                {
                    Id = i,
                    Price = new Random().Next(1, 11),
                    BodyType = (UnitBodyType)new Random().Next(0, 2),
                    Health = new Random().Next(1, 11),
                    Speed = new Random().Next(1, 11),
                    Damage = new Random().Next(1, 11),
                });
        }

        public UnitToBuyData[] GetUnitsInStock() => unitsInStock.ToArray();

        public bool TryBuyUnit(int unitId)
        {
            if (unitsInStock.Any(x => x.Id == unitId) == false)
                throw new Exception(unitId.ToString());

            if(unitExistenceControl.HasPlaceForUnit() == false)
                return false;

            //проверить, есть ли деньги в кошельке

            var unitToBuyData = unitsInStock.First(x => x.Id == unitId);

            //взять деньги из кошелька

            unitsInStock.Remove(unitToBuyData);
            unitExistenceControl.AddBoughtUnit(unitToBuyData, view.GetUnitSpawnPosition());

            return true;
        }
    }
}
