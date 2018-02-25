namespace Starship.Core.Services.Interfaces
{
    public interface IRandomGenerator
    {
        double GenerateDouble(double min, double max);

        int GenerateInt(int max);

        int GenerateInt(int min, int max);

        bool GenerateBool(int probabilityTrue);
    }
}
