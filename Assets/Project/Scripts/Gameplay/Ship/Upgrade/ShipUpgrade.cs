using Gameplay.Game;

namespace Gameplay.Ship.Upgrade
{
    /*
     * тут будет возможность покупки улучшений
     * не будет прямого управления апгрейдами, т.е.
     * к примеру: нужно апгрейднуть пушку, этот класс будет только иметь доступ к пушке из ShipFight класса
     * а если нужно создать новую, то это будет происходить так же в ShipFight
     */
    public class ShipUpgrade
    {
        private readonly GameState gameState;

        public ShipUpgrade(GameState gameState)
        {
            this.gameState = gameState;
        }
    }
}
