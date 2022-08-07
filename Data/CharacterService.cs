using System;
using BackendAPI.Models;
using BackendAPI.Data;

namespace BackendAPI.Data
{
    public class CharacterService : ICharacterService
    {
        private readonly CharacterDb _dbChar;

        public CharacterService(CharacterDb dbChar)
        {
            _dbChar = dbChar;
        }

        public IEnumerable<Character> GetAllCharacters()
        {
            IEnumerable<Character> characters = _dbChar.Characters.ToList<Character>();
            return characters;
        }

        public Character GetCharByName(string name)
        {
            Character character = _dbChar.Characters.FirstOrDefault(c => c.Name == name);
            return character;
        }

        public void AddChar(Character character)
        {
            _dbChar.Characters.Add(character);
            _dbChar.SaveChanges();
        }

        public void RemoveChar(Character character)
        {
            Character existingChar = _dbChar.Characters.FirstOrDefault(c => c.Name == character.Name);
            if (existingChar != null)
            {
                _dbChar.Remove(existingChar);
                _dbChar.SaveChanges();
            }
        }

        public void UpdateChar(Character character)
        {
            Character existingChar = _dbChar.Characters.FirstOrDefault(c => c.Name == character.Name);
            if (existingChar != null)
            {
                _dbChar.Update(existingChar);
            }
        }
    }
}

