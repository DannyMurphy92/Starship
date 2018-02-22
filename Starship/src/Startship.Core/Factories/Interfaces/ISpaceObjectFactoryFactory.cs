namespace Starship.Core.Factories.Interfaces
{
    public interface ISpaceObjectFactoryFactory
    {
        ISpaceObjectFactory CreateFactory(ObjectsEnum type);
    }
}
