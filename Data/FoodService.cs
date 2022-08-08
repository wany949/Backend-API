using System;
using BackendAPI.Models;
using BackendAPI.Data;

namespace BackendAPI.Data
{
    public class FoodService : IFoodService
    {
        private readonly FoodDb dbFood;

        public FoodService(FoodDb dbChar)
        {
            dbFood = dbChar;
        }

        public IEnumerable<Food> GetAllFood()
        {
            IEnumerable<Food> food = dbFood.FoodList.ToList<Food>();
            return food;
        }

        public Food GetFoodByName(string name)
        {
            Food food = dbFood.FoodList.FirstOrDefault(f => f.Name == name);
            return food;
        }

        public List<Food> Get3CourseMeal()
        {
            List<Food> meal = new();

            if (dbFood.FoodList.Count() != 0)
            {
                int totalFoodCount = dbFood.FoodList.Count();
                Random random = new();
                for (int i = 0; i < 3; i++)
                {
                    int randomInRange = random.Next(0, totalFoodCount);
                    Food f = dbFood.FoodList.Skip(randomInRange).Take(1).First();
                    meal.Add(f);
                }
            }

            return meal;
        }

        public void AddFood(Food food)
        {
            Food existingFood = dbFood.FoodList.FirstOrDefault(f => f.Name == food.Name);

            if (existingFood is null)
            {
                dbFood.FoodList.Add(food);
            }
            
            dbFood.SaveChanges();
        }

        public void RemoveFood(Food food)
        {
            Food existingFood = dbFood.FoodList.FirstOrDefault(c => c.Name == food.Name);
            if (existingFood != null)
            {
                dbFood.Remove(existingFood);
                dbFood.SaveChanges();
            }
        }

        public void UpdateFood(Food food)
        {
            Food existingFood = dbFood.FoodList.FirstOrDefault(c => c.Name == food.Name);
            if (existingFood != null)
            {
                dbFood.Update(existingFood);
            }
        }
    }
}

