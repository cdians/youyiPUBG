namespace YouYiApp.model
{
    public class DiscernPoint
    {
        public int x { get; set; }

        public int y { get; set; }

        public int color { get; set; }

        public DiscernPoint(int x, int y, int color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }
}
