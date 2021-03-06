﻿using Starship.Core.Models;

namespace Starship.Core.Factories.Interfaces
{
    public interface IPositionFactory
    {
        Position Create();

        Position CreateFromString(string x, string y, string z);
    }
}
