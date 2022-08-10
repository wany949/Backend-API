using System;
using BackendAPI.Models;
using BackendAPI.Data;

namespace BackendAPI.Data
{
    public class FoodService : IFoodService
    {
        private readonly FoodDb _dbFood;

        public FoodService(FoodDb dbFood)
        {
            _dbFood = dbFood;
        }

        public IEnumerable<Food> GetAllFood()
        {
            IEnumerable<Food> food = _dbFood.FoodList.ToList<Food>();
            return food;
        }

        public Food GetFoodByName(string name)
        {
            Food food = _dbFood.FoodList.FirstOrDefault(f => f.Name == name);
            return food;
        }

        public List<Food> Get3CourseMeal()
        {
            List<Food> meal = new();

            if (_dbFood.FoodList.Count() != 0)
            {
                int totalFoodCount = _dbFood.FoodList.Count();
                Random random = new();
                for (int i = 0; i < 3; i++)
                {
                    int randomInRange = random.Next(0, totalFoodCount);
                    Food f = _dbFood.FoodList.Skip(randomInRange).Take(1).First();
                    meal.Add(f);
                }
            }

            return meal;
        }

        public void AddFood(Food food)
        {
            Food existingFood = _dbFood.FoodList.FirstOrDefault(f => f.Name == food.Name);

            if (existingFood is null)
            {
                _dbFood.FoodList.Add(food);
            }
            
            _dbFood.SaveChanges();
        }

        public void RemoveFood(Food food)
        {
            Food existingFood = _dbFood.FoodList.FirstOrDefault(f => f.Name == food.Name);
            if (existingFood is not null)
            {
                _dbFood.Remove(existingFood);
                _dbFood.SaveChanges();
            }
        }

        public void UpdateFood(Food food)
        {
            Food existingFood = _dbFood.FoodList.FirstOrDefault(f => f.Name == food.Name);
            if (existingFood != null)
            {
                _dbFood.Update(existingFood);
            }
        }
    }
}

