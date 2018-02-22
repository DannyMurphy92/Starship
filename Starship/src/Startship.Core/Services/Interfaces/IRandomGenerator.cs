namespace Starship.Core.Services.Interfaces
{
    public interface IRandomGenerator
    {
        double GenerateDouble();

        bool GenerateBool(int probabilityTrue = 50);
    }
}
