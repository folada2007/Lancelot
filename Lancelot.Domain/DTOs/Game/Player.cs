namespace Lancelot.Shared.DTOs.Game
{
    public class Player
    {
        public float X {  get; set; } = 0;
        public float Y { get; set; } = 400;
        public float SpeedX { get; set; } = 50;
        public float SpeedY { get; set; } = 0;
        public int Size { get; set; } = 100;
        public bool IsAlive { get; set; } = true;
        public bool OnGround { get; set; } = true;
        public bool OnMove { get; set; }
    }
}
