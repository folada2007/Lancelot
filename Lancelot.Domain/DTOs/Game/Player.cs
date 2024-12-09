namespace Lancelot.Shared.DTOs.Game
{
    public class Player
    {
        public int X {  get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Speed { get; set; } = 25;
        public int Size { get; set; } = 100;
        public bool IsAlive { get; set; } = true;
    }
}
