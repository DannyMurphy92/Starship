namespace Starship.Core.Models.Abstracts
{
    public abstract class BaseSpaceObject
    {
        public Position Position { get; protected set; }

        public abstract string ToString();
    }
}
