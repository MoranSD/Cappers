namespace Gameplay.Ship.Fight.Hole
{
    public interface IShipHoleFactory
    {
        ShipHole CreateHoleInZone(int zoneId, ShipFight shipFight);
    }
}
