using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starship.Core
{
    public class Settings
    {
        public int ProbPlanetIsHabitable { get; set; }

        public double MinPlanetArea { get; set; }

        public double MaxPlanetArea { get; set; }

        public int ProbPlanet { get; set; }

        public int Area1Limit { get; set; }

        public int Area2Limit { get; set; }

        public int Area3Limit { get; set; }

        public int Area4Limit { get; set; }

        public int TravelTimeInMins { get; set; }

        public double ColonizationRate { get; set; }

        public double PercentageToColonize { get; set; }
    }
}
