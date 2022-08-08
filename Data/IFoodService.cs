using BackendAPI.Models;

namespace BackendAPI.Data
{
    public interface IFoodService
    {
        public IEnumerable<Food> GetAllFood();
        public Food GetFoodByName(string name);
        public void AddFood(Food food);
        public void RemoveFood(Food food);
        public void UpdateFood(Food food);
    }
}
