using BusinessLogic.Contracts;
using BusinessLogic.Model;
using Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class GameDataGeneratorService : IGameDataGeneratorService
    {
        const int weaponsCount = 3;

        private readonly ContextBizagiMatch _context;

        public GameDataGeneratorService(ContextBizagiMatch context)
        {
            _context = context;
        }

        public List<Weapon> GenerateCharactersWeapons(Character character)
        {
            Random rand = new Random();
            var result = new List<Weapon>();

            for(var count = 0; count < weaponsCount; count++)
            {
                var elementTypeVal = rand.Next(1, 5);

                result.Add(new Weapon
                {
                    Id = Guid.NewGuid(),
                    Name = count.ToString(),
                    Attack = rand.Next(10, 50),
                    Character = character,
                    ElementType = elementTypeVal == 1 ? ElementType.Earth : elementTypeVal == 2 ? ElementType.Water : elementTypeVal == 3 ? ElementType.Electric : elementTypeVal == 4 ? ElementType.Fire : ElementType.Air,
                });
            }


            return result;
        }

        public List<Character> GenerateGameCharacters(Game game)
        {
            var result = new List<Character>();
            var playersByGame = game.Players;
            Random rand = new Random();

            foreach (var playerByGame in playersByGame)
            {
                var player = _context.Players.FirstOrDefault(p => p.Id.Equals(playerByGame.PlayerId));
                var elementTypeVal = rand.Next(1, 5);

                result.Add(new Character
                {
                    Id = Guid.NewGuid(),
                    Name = playerByGame.Player.Name + " Character",
                    Attack = rand.Next(100, 200),
                    Life = rand.Next(2000, 4000),
                    CharacterType = rand.Next(1, 2) == 1 ? CharacterType.Monster : CharacterType.Robot,
                    ElementType = elementTypeVal == 1 ? ElementType.Earth : elementTypeVal == 2 ? ElementType.Water : elementTypeVal == 3 ?ElementType.Electric : elementTypeVal == 4 ? ElementType.Fire : ElementType.Air,
                    Player = playerByGame.Player,
                    
                }) ;
            }

            return result;
        }
    }
}
