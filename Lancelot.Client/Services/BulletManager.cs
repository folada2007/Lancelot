using Lancelot.Shared.DTOs.Game;

namespace Lancelot.Client.Services
{
    public class BulletManager
    {
        private Task? _isRecharge;

        static DateTime LastShot = DateTime.MinValue;

        private readonly GameStateManager _gameState;
        private readonly EnemiesManager _enemiesManager;

        public BulletManager(GameStateManager gameState, EnemiesManager enemiesManager)
        {
            _enemiesManager = enemiesManager;
            _gameState = gameState;
        }

        public async Task Shot()
        {
            if (DateTime.Now - LastShot < TimeSpan.FromMilliseconds(100))
            {
                return;
            }
            LastShot = DateTime.Now;

            PlayerManager.BulletCount--;
            var bullet = new Bullets
            {
                X = _gameState.player.X + _gameState.player.Size / 2,
                Y = _gameState.player.Y + _gameState.player.Size / 2,
                IsAlive = true,
                Speed = 12,
            };
            _gameState.bullets.Add(bullet);


            while (bullet.IsAlive)
            {
                bullet.X += bullet.Speed;

               _enemiesManager.IsCollision(bullet);

                await Task.Delay(16);

                if (bullet.X > PlayerManager.WIDTH)
                {
                    bullet.IsAlive = false;
                }

                _gameState.GameStateUpdate();
            }

            _gameState.bullets.RemoveAll(a => !a.IsAlive);
        }

        public Task StartRecharge()
        {
            if (_isRecharge == null || _isRecharge.IsCompleted)
            {
                _isRecharge = Recharge();
            }
            return _isRecharge;
        }

        private async Task Recharge()
        {
            if (PlayerManager.BulletCount < PlayerManager.BULLETCOUNT)
            {
                for (int i = 0; i < PlayerManager.BULLETCOUNT; i++)
                {
                    if (PlayerManager.BulletCount >= PlayerManager.BULLETCOUNT)
                        break;

                    await Task.Delay(200);
                    PlayerManager.BulletCount++;
                    _gameState.GameStateUpdate();
                }
            }
        }
    }
}
