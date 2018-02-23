using System;
using Starship.Core.Factories.Interfaces;
using Starship.Core.Models;

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
                var position = positionFactory.CreateFromString(arguments[1], arguments[2], arguments[3]);
                
                return new Monster(position);
                
            }
            throw new ArgumentException("Input is not a valid argument");
        }
    }
}
