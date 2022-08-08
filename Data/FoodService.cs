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

