﻿using System;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Services
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random rdn;

        public RandomGenerator()
        {
            rdn = new Random();
        }
        public double GenerateDouble()
        {
            return rdn.NextDouble();
        }

        public bool GenerateBool(int probabilityTrue)
        {
            return rdn.Next(100) < probabilityTrue;
        }
    }
}
