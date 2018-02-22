using Starship.Core.Models.Interfaces;

namespace Starship.Core.Models
{
    public class SpacePlanet : ISpaceObject
    {
        public double XCoor { get; set; }

        public double ZCoor { get; set; }

        public double YCoor { get; set; }

        public bool IsHabitable { get; set; }

        public double Area { get; set; }
    }
}
