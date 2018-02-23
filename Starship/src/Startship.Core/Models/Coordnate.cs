namespace Starship.Core.Models
{
    public class Coordnate
    {
        public Coordnate(int area1, int area2, int area3, int area4)
        {
            Area1 = area1;
            Area2 = area2;
            Area3 = area3;
            Area4 = area4;
        }
        public int Area1 { get; }

        public int Area2 { get; }

        public int Area3 { get; }

        public int Area4 { get; }
    }
}
