using Lancelot.Client.Enum.Game;
using Microsoft.AspNetCore.Components.Web;

namespace Lancelot.Client.Services
{
    public class PlayerManager
    {
        public const int WIDTH = 1000;
        public const int HEIGHT = 500;
        public const int BULLETCOUNT = 20;

        public static int BulletCount = BULLETCOUNT;
        public Direction? _direction;

        private readonly GameStateManager _gameState;
        private readonly BulletManager _bulletManager;

        public PlayerManager(GameStateManager gameStateManager, BulletManager bulletManager)
        {
            _gameState = gameStateManager;
            _bulletManager = bulletManager;
        }

        public void Move(KeyboardEventArgs e)
        {
            switch (e.Key.ToLower())
            {
                case "w":
                    SetDirection(Direction.up);
                    break;
                case "a":
                    SetDirection(Direction.left);
                    break;
                case "d":
                    SetDirection(Direction.right);
                    break;
                case "r":
                    break;
                case "f":
                    _bulletManager.Shot();
                    break;
            }
        }

        public void StopMove() 
        {
            _direction = null;
        }

        private void SetDirection(Direction direction) 
        {
            _direction = direction;
        }

        public void DirectionOfMovement(Direction? direction) 
        {
            GravityHandler();

            if (direction == null)
                return;

            switch (direction) 
            {
                case Direction.left:
                    if (_gameState.player.X > 0)
                        _gameState.player.X -= _gameState.player.SpeedX / 5;
                    break;
                case Direction.right:
                    if (_gameState.player.X + _gameState.player.Size < WIDTH)
                        _gameState.player.X += _gameState.player.SpeedX / 5;
                    break;
                case Direction.up:
                    Jump();
                    break;
            }
        }

        private void Jump() 
        {
            if (_gameState.player.OnGround) 
            {
                _gameState.player.SpeedY = -12;
                _gameState.player.Y += _gameState.player.SpeedY;
                _gameState.player.OnGround = false;
            }
        }

        private void GravityHandler() 
        {
            if (!_gameState.player.OnGround) 
            {
                _gameState.player.Y += _gameState.player.SpeedY;
                
                _gameState.player.SpeedY += 1;
                

                if (_gameState.player.Y >= 400) 
                {
                    _gameState.player.Y = 400;
                    _gameState.player.OnGround = true;
                    _gameState.player.SpeedY = 0;
                }
            }
        }
    }
}
