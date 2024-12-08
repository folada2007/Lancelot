using Lancelot.Shared.DTOs.Game;

namespace Lancelot.Client.Services
{
    public class GameStateManager
    {
        public event Action? UpdateState;
        public List<Bullets> bullets = new();
        public Player player = new();
        public List<Enemy> enemies = new();

        public void GameStateUpdate()
        {
            UpdateState?.Invoke();
        }
    }
}
