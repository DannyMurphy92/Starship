using Starship.Core.Models.Interfaces;

namespace Starship.Core.Models
{
    public class Planet : ISpaceObject
    {
        public Planet(Position position, bool isHabitable, double area)
        {
            Position = position;
            IsHabitable = isHabitable;
            Area = area;
        }

        public bool IsHabitable { get; }

        public double Area { get; }

        public Position Position { get; }
    }
}
