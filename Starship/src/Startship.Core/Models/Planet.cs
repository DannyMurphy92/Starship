using Starship.Core.Models.Abstracts;

namespace Starship.Core.Models
{
    public class Planet : BaseSpaceObject
    {
        public Planet(Position position, bool isHabitable, double area)
        {
            Position = position;
            IsHabitable = isHabitable;
            Area = area;
        }

        public bool IsHabitable { get; }

        public double Area { get; }
        
        public override string ToString()
        {
            return $"{ObjectsEnum.Planet}, {Position.ToString()}, {IsHabitable}, {Area}";
        }
    }
}
