using Starship.Core.Models.Abstracts;

namespace Starship.Core.Dtos
{
    public class SortResult
    {
        public BaseSpaceObject Object { get; set; }

        public double Distance { get; set; }
    }
}
