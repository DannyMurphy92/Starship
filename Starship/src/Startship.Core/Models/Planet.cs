using Starship.Core.Models.Interfaces;

namespace Starship.Core.Models
{
    public class Planet : ISpaceObject
    {
        public bool IsHabitable { get; set; }

        public double Area { get; set; }

        public Position Position { get; set; }
    }
}
