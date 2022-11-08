using Model;

namespace BusinessLogic.Contracts
{
    public interface IGameDataGeneratorService
    {
        List<Character> GenerateGameCharacters(Game game);
        List<Weapon> GenerateCharactersWeapons(Character character);
    }
}