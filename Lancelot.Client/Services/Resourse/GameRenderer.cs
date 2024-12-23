using Lancelot.Shared.DTOs.Game;
using Microsoft.AspNetCore.Components;
using SkiaSharp;

namespace Lancelot.Client.Services.Resourse
{
    public class GameRenderer
    {
        SKBitmap? PlayerSprite;
        SKBitmap? EnemySprite;
        SKBitmap? BulletSprite;
        List<SKBitmap> AllBackgroundSprite = new();
        List<SKImage> PreRenderList = new();

        int CurrentBackgroundFrame = 0;
        int DelaySwitchFrame = 0;

        private readonly ResourceLoader _resourceLoader;
        private readonly GameStateManager _gameStateManager;

        public GameRenderer(ResourceLoader resourceLoader, GameStateManager gameStateManager)
        {
            _resourceLoader = resourceLoader;
            _gameStateManager = gameStateManager;
        }

        public async Task LoadResources() 
        {
            PlayerSprite = await _resourceLoader.LoadBitmapAsync("/img/Lancelot-alfa.png");
            EnemySprite = await _resourceLoader.LoadBitmapAsync("/img/Glazgo.png");
            BulletSprite = await _resourceLoader.LoadBitmapAsync("/img/Bullet.png");

            for (var i = 0; i < 53; i++)
            {
                var bitmap = await _resourceLoader.LoadBitmapAsync($"/img/Background/{i}-0000.jpg");
                AllBackgroundSprite.Add(bitmap);
            }
            PreRenderBackground();
        }

        private void DrawPlayer(SKCanvas canvas)
        {
            var player = _gameStateManager.player;

            if (PlayerSprite == null)
                return;

            var drawPlayer = new SKRect(player.X,player.Y,player.X + player.Size,player.Y + player.Size);
            canvas.DrawBitmap(PlayerSprite, drawPlayer);
        }

        private void DrawEnemy(SKCanvas canvas) 
        {
            if (EnemySprite == null)
                return;

            foreach (var enemy in _gameStateManager.enemies) 
            {
                var drawEnemy = new SKRect(enemy.X,enemy.Y,enemy.X + enemy.Size,enemy.Y + enemy.Size);
                canvas.DrawBitmap(EnemySprite,drawEnemy);
            }
           
        }

        private void DrawBullet(SKCanvas canvas) 
        {
            if (BulletSprite == null)
                return;

            foreach (var bullet in _gameStateManager.bullets) 
            {
                var drawBullet = new SKRect(bullet.X,bullet.Y,bullet.X + 15,bullet.Y + 15);
                canvas.DrawBitmap(BulletSprite,drawBullet);
            }
        }

        private void DrawBackground(SKCanvas canvas)
        {
            var drawRect = new SKRect(0, 0, PlayerManager.WIDTH, PlayerManager.HEIGHT);
            canvas.DrawImage(PreRenderList[CurrentBackgroundFrame],drawRect);
        }

        private void PreRenderBackground() 
        {
            using var surface = SKSurface.Create(new SKImageInfo(PlayerManager.WIDTH,PlayerManager.HEIGHT));
            using var canvas = surface.Canvas;

            canvas.Clear(SKColors.Transparent);

            foreach (var frame in AllBackgroundSprite) 
            {
                canvas.DrawBitmap(frame, new SKRect(0,0, PlayerManager.WIDTH, PlayerManager.HEIGHT));
                PreRenderList.Add(surface.Snapshot());
            }
        }
       
        public void UpdateBackground() 
        {
            if (DelaySwitchFrame == 3) 
            {
                CurrentBackgroundFrame = (CurrentBackgroundFrame + 1) % 53;
                DelaySwitchFrame = 0;
            }
            DelaySwitchFrame++;
        }

        public void Render(SKCanvas canvas) 
        {
            canvas.Clear(SKColors.Black);
            if (AllBackgroundSprite.Count > 0)
                DrawBackground(canvas);

            DrawPlayer(canvas);
            DrawEnemy(canvas);
            DrawBullet(canvas);
        }

    }
}
