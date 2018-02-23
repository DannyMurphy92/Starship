using Starship.Core.Models.Abstracts;

namespace Starship.Core.Models
{
    public class Position  : BaseStringSerialiserObject
    {
        public Position(Coordinate x, Coordinate y, Coordinate z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Coordinate X { get; }

        public Coordinate Y { get; }

        public Coordinate Z { get; }

        public override string ToString()
        {
            return $"{X.ToString()}, {Y.ToString()}, {Z.ToString()}";
        }
    }
}
