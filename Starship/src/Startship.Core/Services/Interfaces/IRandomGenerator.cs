namespace Starship.Core.Services.Interfaces
{
    public interface IRandomGenerator
    {
        double GenerateDouble();

        int GenerateInt(int max, int min = 0);

        bool GenerateBool(int probabilityTrue);
    }
}
