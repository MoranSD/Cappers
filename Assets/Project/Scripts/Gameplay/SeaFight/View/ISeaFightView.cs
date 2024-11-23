using System.Threading.Tasks;

namespace Gameplay.SeaFight.View
{
    public interface ISeaFightView
    {
        Task ShowNewShip();
        void HideCurrentShip();
        object GetCurrentShipView();
    }
}
