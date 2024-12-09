using Microsoft.AspNetCore.Components.Web;

namespace Lancelot.Client.Services
{
    public class PlayerManager
    {
        public const int WIDTH = 1000;
        public const int HEIGHT = 500;
        public const int BULLETCOUNT = 20;
        public static int BulletCount = BULLETCOUNT;

        private readonly GameStateManager _gameState;
        private readonly BulletManager _bulletManager;

        public PlayerManager(GameStateManager gameStateManager, BulletManager bulletManager)
        {
            _gameState = gameStateManager;
            _bulletManager = bulletManager;
        }

        public async Task Move(KeyboardEventArgs e)
        {
            switch (e.Key.ToLower())
            {
                case "w":
                    if (_gameState.player.Y > 0) _gameState.player.Y -= _gameState.player.Speed;
                    break;
                case "a":
                    if (_gameState.player.X > 0) _gameState.player.X -= _gameState.player.Speed;
                    break;
                case "s":
                    if (_gameState.player.Y + _gameState.player.Size < HEIGHT) _gameState.player.Y += _gameState.player.Speed;
                    break;
                case "d":
                    if (_gameState.player.X + _gameState.player.Size < WIDTH) _gameState.player.X += _gameState.player.Speed;
                    break;
                case " ":
                    if (BulletCount > 0 && BulletCount <= BULLETCOUNT)
                        await _bulletManager.Shot();
                    break;
                case "r":
                    await _bulletManager.StartRecharge();
                    break;
            }
        }
    }
}
