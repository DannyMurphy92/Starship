namespace Starship.Core.Models.Abstracts
{
    public abstract class BaseSpaceObject : BaseStringSerialiserObject
    {
        public Position Position { get; protected set; }
    }
}
