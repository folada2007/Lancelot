﻿using Lancelot.Shared.DTOs.Game;
using System;

namespace Lancelot.Client.Services
{
    public class EnemiesManager
    {
        private readonly GameStateManager _gameStateManager;

        public EnemiesManager(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public async Task AddEnemy()
        {
            await Task.Delay(3000);

            Random rndPosition = new Random();

            var enemy = new Enemy
            {
                X = rndPosition.Next(250, 450),
                Y = rndPosition.Next(0, 450),
                Size = 100,
                HitPoint = 3,
                IsAlive = true
            };

            if (_gameStateManager.enemies.Count > 0)
            {
                foreach (var enemies in _gameStateManager.enemies)
                {
                    if (enemy.X < enemies.X + enemies.Size &&
                   enemy.X + 50 > enemies.X &&
                   enemy.Y < enemies.Y + enemies.Size &&
                   enemy.Y + 50 > enemies.Y)
                    {
                        enemy.IsAlive = false;
                    }
                }
            }

            if (_gameStateManager.enemies.Count < 3 && enemy.IsAlive)
                _gameStateManager.enemies.Add(enemy);

            _gameStateManager.GameStateUpdate();
        }

        public void IsCollision(Bullets bullet)
        {
            foreach (var enemy in _gameStateManager.enemies)
            {
                if (bullet.X < enemy.X + enemy.Size &&
                    bullet.X + 5 > enemy.X &&
                    bullet.Y < enemy.Y + enemy.Size &&
                    bullet.Y + 5 > enemy.Y)
                {
                    bullet.IsAlive = false;

                    if (enemy.HitPoint > 0)
                    {
                        enemy.HitPoint--;
                    }
                    if (enemy.HitPoint == 0) 
                    {
                        enemy.IsAlive = false;
                    }
                }
            }
            _gameStateManager.enemies.RemoveAll(e => !e.IsAlive);
        }
    }
}
