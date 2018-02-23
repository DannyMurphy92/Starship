using System;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;
using Starship.Core.Services.Interfaces;

namespace Starship.Core.Factories
{
    public class MonsterFactory : IMonsterFactory
    {
        private readonly IPositionFactory positionFactory;

        public MonsterFactory(IPositionFactory positionFactory)
        {
            this.positionFactory = positionFactory;
        }

        public Monster Create()
        {
            return new Monster(positionFactory.Create());
        }

        //Assumption made that string input will always be in same format, comma seperated string
        public Monster CreateFromString(string input)
        {
            var valid = true;
            var arguments = input.Split(',');

            if (arguments.Length == 4)
            {
                valid &= double.TryParse(arguments[1], out var x);
                valid &= double.TryParse(arguments[2], out var y);
                valid &= double.TryParse(arguments[3], out var z);
                if (valid)
                {
                    var position = new Position(x, y, z);
                    return new Monster(position);
                }
            }
            throw new ArgumentException("Input is not a valid argument");
        }
    }
}
