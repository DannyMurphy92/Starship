using Starship.Core.Models;
using Starship.Core.Models.Abstracts;

namespace Starship.Core.Dtos
{
    public class SortResult
    {
        public Planet Planet { get; set; }

        public double Distance { get; set; }
    }
}
