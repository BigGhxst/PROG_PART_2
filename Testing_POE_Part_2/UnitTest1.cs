using NUnit.Framework;
using System.Collections.Generic;


namespace RecipeManager.Tests
{
    [TestFixture]
    public class RecipeTests
    {
        [Test]
        public void CalculateTotalCalories_ReturnsCorrectTotal()
        {
            // Arrange
            Recipe recipe = new Recipe("Test Recipe");
            recipe.AddIngredient("Ingredient 1", "100", "grams", 50, "Food Group 1");
            recipe.AddIngredient("Ingredient 2", "200", "grams", 70, "Food Group 2");

            recipe.AddIngredient("Ingredient 3", "150", "grams", 80, "Food Group 3");

            // Expected total calories: 50 + 70 + 80 = 200
            int expectedTotalCalories = 50 + 70 + 80;

            // Act
            int actualTotalCalories = recipe.GetTotalCalories();

            // Assert
            Assert.AreEqual(expectedTotalCalories, actualTotalCalories);
        }
    }
}