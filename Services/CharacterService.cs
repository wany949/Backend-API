using System;
using BackendAPI.Models;
namespace BackendAPI.Services;

public static class CharacterService
{
    static List<Character> Characters { get; }

    static CharacterService()
    {
        Characters = new List<Character> {};
    }

    public static List<Character> GetAll() => Characters;

    public static Character? Get(string Name) => Characters.FirstOrDefault(c => c.Name == Name);

    public static void Add(Character character)
    {
        Characters.Add(character);
    }

    public static void Delete(string Name)
    {
        var character = Get(Name);
        if (character is null) 
            return;

        Characters.Remove(character);
    }

    public static void Update(Character character)
    {
        var index = Characters.FindIndex(c => c.Name == character.Name);
        if (index == -1)
            return;

        Characters[index] = character;
    }
}