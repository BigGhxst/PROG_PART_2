using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG_PART_2
{
    // Delegate for notifying the user when a recipe exceeds 300 calories
    public delegate void RecipeNotification(string recipeName);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<WELCOME TO RECIPE MANAGER!>>>>>>>>>>>>>>>>>>>>");

            List<Recipe> recipes = new List<Recipe>();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=================================================================");
                Console.WriteLine("\nRecipe Manager Menu:");
                Console.WriteLine("1. Enter Recipe");
                Console.WriteLine("2. Display Recipes");
                Console.WriteLine("3. Scale Recipe");
                Console.WriteLine("4. Reset Quantities");
                Console.WriteLine("5. Clear Data");
                Console.WriteLine("6. Exit");
                Console.WriteLine("=================================================================");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EnterRecipe(recipes);
                        Console.WriteLine("=================================================================");
                        break;
                    case "2":
                        DisplayAllRecipes(recipes);
                        Console.WriteLine("=================================================================");
                        break;
                    case "3":
                        ScaleRecipe(recipes);
                        Console.WriteLine("=================================================================");
                        break;
                    case "4":
                        ResetQuantities(recipes);
                        Console.WriteLine("=================================================================");
                        break;
                    case "5":
                        ClearData(recipes);
                        Console.WriteLine("=================================================================");
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void EnterRecipe(List<Recipe> recipes)
        {
            Console.WriteLine("\nEnter Recipe Details:");
            Console.Write("Recipe Name: ");
            string name = Console.ReadLine();

            Recipe recipe = new Recipe(name);

            Console.WriteLine("Enter Ingredients for {0}:", name);
            bool addIngredients = true;
            while (addIngredients)
            {
                Console.Write("Please enter Ingredient Name: ");
                string ingredientName = Console.ReadLine();

                Console.Write("Quantity: ");
                string quantity = Console.ReadLine();

                Console.Write("Unit of Measurement: ");
                string unit = Console.ReadLine();

                Console.Write("Number of Calories: ");
                int calories;

                while (!int.TryParse(Console.ReadLine(), out calories) || calories < 0)
                {
                    Console.WriteLine("You have entered an invalid input. Please enter a non-negative integer for calories.");
                }

                Console.Write("Food Group: ");
                string foodGroup = Console.ReadLine();

                recipe.AddIngredient(ingredientName, quantity, unit, calories, foodGroup);

                Console.Write("Add another ingredient? (yes/no): ");
                addIngredients = Console.ReadLine().ToLower() == "yes";
            }

            recipes.Add(recipe);
            Console.WriteLine("Recipe '{0}' added successfully!", name);
        }

        static void DisplayAllRecipes(List<Recipe> recipes)
        {
            Console.WriteLine("\nDisplay Options:");
            Console.WriteLine("1. Display All Recipes");
            Console.WriteLine("2. Search for a Specific Recipe");
            Console.Write("Choose an option: ");
            string displayOption = Console.ReadLine();

            switch (displayOption)
            {
                case "1":
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("No recipes found.");
                        return;
                    }

                    Console.WriteLine("\nAll Recipes:");
                    var sortedRecipes = recipes.OrderBy(r => r.Name);
                    foreach (var recipe in sortedRecipes)
                    {
                        recipe.DisplayRecipeDetails();
                    }
                    break;
                case "2":
                    SearchRecipe(recipes);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Displaying all recipes by default.");
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("No recipes found.");
                        return;
                    }

                    Console.WriteLine("\nAll Recipes:");
                    var sortedRecipesDefault = recipes.OrderBy(r => r.Name);
                    foreach (var recipe in sortedRecipesDefault)
                    {
                        recipe.DisplayRecipeDetails();
                    }
                    break;
            }
        }

        static void SearchRecipe(List<Recipe> recipes)
        {
            Console.Write("Enter the name of the recipe to search for: ");
            string searchName = Console.ReadLine();

            var foundRecipes = recipes.Where(r => r.Name.ToLower().Contains(searchName.ToLower()));
            if (foundRecipes.Any())
            {
                Console.WriteLine("\nFound Recipes:");
                foreach (var recipe in foundRecipes)
                {
                    recipe.DisplayRecipeDetails();
                }
            }
            else
            {
                Console.WriteLine("No recipes found with the provided name.");
            }
        }

        static void ScaleRecipe(List<Recipe> recipes)
        {
            Console.Write("\nEnter the name of the recipe to scale: ");
            string recipeName = Console.ReadLine();

            Recipe selectedRecipe = recipes.Find(r => r.Name == recipeName);
            if (selectedRecipe != null)
            {
                selectedRecipe.ScaleRecipe();
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        static void ResetQuantities(List<Recipe> recipes)
        {
            Console.Write("\nEnter the name of the recipe to reset quantities: ");
            string recipeName = Console.ReadLine();

            Recipe selectedRecipe = recipes.Find(r => r.Name == recipeName);
            if (selectedRecipe != null)
            {
                selectedRecipe.ResetQuantities();
            }
            else
            {
             
                Console.WriteLine("There is no recipe found.");
            }
        }

        static void ClearData(List<Recipe> recipes)
        {
            recipes.Clear();
            Console.WriteLine("All recipe data has been cleared.");
        }
    }

    class Recipe
    {
        public string Name { get; }
        private List<Ingredient> ingredients = new List<Ingredient>();
        private int totalCalories; // Total calories for the recipe

        public Recipe(string name)
        {
            Name = name;
        }

        // Event for notifying the user when a recipe exceeds 300 calories
        public event RecipeNotification NotifyCalorieExceedance;

        public void AddIngredient(string name, string quantity, string unit, int calories, string foodGroup)
        {
            ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            totalCalories += calories;

            // Notify if total calories exceed 300
            if (totalCalories > 300)
            {
                NotifyCalorieExceedance?.Invoke(Name);
            }
        }

        public void DisplayRecipeDetails()
        {
            Console.WriteLine("\nRecipe: {0}", Name);
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine("{0}", ingredient);
            }

            // Set text color based on total calories
            if (totalCalories > 300)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("WARNING!! WARNING!! YOUR TOTAL CALORIES HAVE EXCEEDED 300");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Your Total Calories: {0}", totalCalories);
            }

            // Reset text color to default
            Console.ResetColor();
        }


        public void ScaleRecipe()
        {
            Console.Write("\nEnter scaling factor (0.5, 2, or 3): ");
            double factor;
            while (!double.TryParse(Console.ReadLine(), out factor) || (factor != 0.5 && factor != 2 && factor != 3))
            {
                Console.WriteLine("You have entered an invalid input. Please enter 0.5, 2, or 3.");
            }

            foreach (var ingredient in ingredients)
            {
                ingredient.ScaleQuantity(factor);
            }

            // Recalculate total calories after scaling
            totalCalories = CalculateTotalCalories();
            Console.WriteLine("Your Recipe has been scaled by a factor of {0}.", factor);
            Console.WriteLine("Your total Calories: {0}", totalCalories);
        }

        public void ResetQuantities()
        {
            // Reset quantities of all ingredients
            foreach (var ingredient in ingredients)
            {
                ingredient.ResetQuantity();
            }

            // Recalculate total calories after resetting quantities
            totalCalories = CalculateTotalCalories();
            Console.WriteLine("Your Quantities are reset to their original values.");
            Console.WriteLine("Your total Calories: {0}", totalCalories);
        }

        private int CalculateTotalCalories()
        {
            int total = 0;
            foreach (var ingredient in ingredients)
            {
                total += ingredient.Calories;
            }
            return total;
        }
    }

    class Ingredient
    {
        public string Name { get; }
        public double Quantity { get; private set; }
        public string Unit { get; }
        public int Calories { get; }
        public string FoodGroup { get; }
        private double originalQuantity; // Store original quantity for resetting

        public Ingredient(string name, string quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            Quantity = Convert.ToDouble(quantity);
            originalQuantity = Quantity; // Store original quantity
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        public void ScaleQuantity(double factor)
        {
            Quantity *= factor;
        }

        public void ResetQuantity()
        {
            Quantity = originalQuantity; // Reset quantity to original value
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2}", Name, Quantity, Unit);
        }
    }
}
