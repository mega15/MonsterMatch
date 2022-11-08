// See https://aka.ms/new-console-template for more information

using BusinessLogic;
using BusinessLogic.Contracts;
using Data;
using BusinessLogic.Model;
using Microsoft.EntityFrameworkCore;
using Model;

ContextBizagiMatch _context = null;
IGameDataGeneratorService _gameDataGeneratorService = null;
IMatchesResolverService _matchesResolverService = null;

Console.WriteLine("Welcome to match game console");
Console.WriteLine("Please select an option");

string option = string.Empty;
do
{
    Console.WriteLine("1. Generate character for players");
    Console.WriteLine("2. Resolve game");
    Console.WriteLine();
    Console.WriteLine("9. Exit");

    Console.WriteLine();
    option = Console.ReadLine();

    switch (option)
    {
        case "1":
            GenerateGamePlayersData();
            break;
        case "2":
            ResolveGame();

            break;
    }

} while (option != null && option != "9");

Console.WriteLine("Bye!!!");


void GenerateGamePlayersData()
{
    var activeGame = _context.Games.Include(g => g.Players).FirstOrDefault(g => !g.IsClosed);
    var characters = _gameDataGeneratorService.GenerateGameCharacters(activeGame);

    _context.Characters.AddRange(characters);

    foreach (var character in characters)
    {
        var characterWeapons = _gameDataGeneratorService.GenerateCharactersWeapons(character);
        _context.Weapons.AddRange(characterWeapons);
    }

    for (int i = 0; i < characters.Count; i++)
    {
        PlayersByGame playerByGame = new PlayersByGame()
        {
            Character = characters[i],
            Game = activeGame,
            GameId = activeGame.Id,
            Player = characters[i].Player,
            PlayerId = characters[i].Player.Id,
            Money = 1000,
            Points = 0,
        };

        _context.PlayersByGames.Add(playerByGame);
    }

    for (int i = 0; i < characters.Count; i++)
    {
        for (int j = i + 1; j < characters.Count; j++)
        {
            Character character1 = characters[i];
            Character character2 = characters[j];

            _context.Matches.Add(new Match()
            {
                Id = Guid.NewGuid(),
                Name = $"{character1.Name} VS {character2.Name}",
                Character1 = character1,
                Character2 = character2,
            });
        }
    }

    _context.SaveChanges();
}

void ResolveGame()
{
    var activeGame = _context.Games.Include(g => g.Players).FirstOrDefault(g => !g.IsClosed);
    var matches = _context.Matches.Include(m => m.Game).Include(m => m.Character1).Include(m => m.Character2).Include(m => m.WeaponCharacter1).Include(m => m.WeaponCharacter2).Where(m => m.Game.Id == activeGame.Id);

    foreach (var match in matches)
    {
        var figther1 = FigtherFactory.Create(match.Character1, match.WeaponCharacter1);
        var figther2 = FigtherFactory.Create(match.Character1, match.WeaponCharacter1);

        var winner = _matchesResolverService.ResolveMatch(figther1, figther2);

        match.Winner = winner;
        _context.Entry(match).State = EntityState.Modified;

        //TODO:logica calculo de apuestas
    }
}