using Lancelot.Shared.DTOs.Game;
using Microsoft.AspNetCore.Components.Web;

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
            PlayerManager.BulletCount--;

            if (DateTime.Now - LastShot < TimeSpan.FromMilliseconds(200))
            {
                return;
            }
            LastShot = DateTime.Now;

            var bullet = CreateBullet();
            await MoveBullets(bullet);
        }

        private async Task MoveBullets(Bullets bullet) 
        {
            while (bullet.IsAlive)
            {
                bullet.X += bullet.Speed;
                await Task.Delay(16);

                _enemiesManager.IsCollision(bullet);

                if (bullet.X > PlayerManager.WIDTH) 
                {
                    bullet.IsAlive = false;
                }
                _gameState.GameStateUpdate();
            }
            _gameState.bullets.RemoveAll(b => !b.IsAlive);
        }

        private Bullets CreateBullet() 
        {
            var bullet = new Bullets
            {
                X = _gameState.player.X + _gameState.player.Size / 2,
                Y = _gameState.player.Y + _gameState.player.Size / 2,
                IsAlive = true,
                Speed = 25,
            };

            _gameState.bullets.Add(bullet);
            return bullet;
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
