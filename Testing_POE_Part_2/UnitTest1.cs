using NUnit.Framework;
using System.Collections.Generic;

namespace RecipeTests
{
    [TestFixture]
    public class RecipeTests
    {
        
        private string Name;
        private List<Ingredient> ingredients;

        [Test]
        public void Test_TotalCaloriesCalculation()
        {
            // Arrange
            var recipe = new RecipeTests(); 
            recipe.Name = "Test Recipe"; 
            recipe.ingredients = new List<Ingredient>();

            // Create the first ingredient with specific properties
            var ingredient1 = new Ingredient
            {
                Name = "Ingredient 1",
                Quantity = 100, 
                Unit = "g",
                Calories = 50,
                FoodGroup = "Test Group",
                OriginalQuantity = 100 
            };

            
            var ingredient2 = new Ingredient
            {
                Name = "Ingredient 2",
                Quantity = 200, 
                Unit = "g",
                Calories = 100,
                FoodGroup = "Test Group", 
                OriginalQuantity = 200 
            };

            
            recipe.ingredients.Add(ingredient1);
            recipe.ingredients.Add(ingredient2);

            // Act
            // Calculate the total calories by summing up the calories of all ingredients in the list
            int totalCalories = recipe.ingredients.Sum(i => i.Calories);

            // Assert
            // Verify that the total calories calculated is as expected (150 in this case)
            Assert.AreEqual(150, totalCalories);
        }
    }
}

