using BackendAPI.Models;

namespace BackendAPI.Data
{
    public interface ICharacterService
    {
        public IEnumerable<Character> GetAllCharacters();
        public Character GetCharByName(string name);
        public void AddChar(Character character);
        public void RemoveChar(Character character);
        public void UpdateChar(Character character);
    }
}
