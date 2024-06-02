using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecipeManager
{
    // Delegate for notifying the user when a recipe exceeds 300 calories
    //This delegate is used to define the structure of methods that can be invoked when a recipe exceeds 300 calories.
    //It's used to define an event in the Recipe class (NotifyCalorieExceedance), which is invoked when the total calories
    //of a recipe exceed a certain threshold.
    public delegate void RecipeNotification(string recipeName);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<WELCOME TO RECIPE MANAGER!>>>>>>>>>>>>>>>>>>>>");

            // Generic collection to store all recipes
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

        // Method to enter a new recipe
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

                // Add ingredient to the recipe
                recipe.AddIngredient(ingredientName, quantity, unit, calories, foodGroup);

                Console.Write("Add another ingredient? (yes/no): ");
                addIngredients = Console.ReadLine().ToLower() == "yes";
            }

            // Add recipe to the collection
            recipes.Add(recipe);
            Console.WriteLine("Recipe '{0}' added successfully!", name);
        }

        // Method to display all recipes
        static void DisplayAllRecipes(List<Recipe> recipes)
        {
            Console.WriteLine("\nDisplay Options:");
            Console.WriteLine("1. Display All Recipes");
            Console.WriteLine("2. Search for a Recipe");
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

        // Method to search for a specific recipe by name
        static void SearchRecipe(List<Recipe> recipes)
        {
            Console.Write("Enter the name of the recipe to search for: ");
            string searchName = Console.ReadLine();
            
            //foundRecipes will be an enumerable collection of recipes that match the search condition
            var foundRecipes = recipes.Where(r => r.Name.ToLower().Contains(searchName.ToLower()));
            // The condition r.Name.ToLower().Contains(searchName.ToLower()) checks if the name
            // of the recipe r.Name contains the search string searchName, ignoring case
            // (both are converted to lowercase for a case-insensitive comparison).
                        if (foundRecipes.Any())
            {
                Console.WriteLine("\nFound Recipes:");
                // checks if there are any recipes in the foundRecipes collection.
                //It iterates over each found recipe (foreach (var recipe in foundRecipes))
                //and calls the DisplayRecipeDetails method on each one to print its details.
                foreach (var recipe in foundRecipes)
                {
                    recipe.DisplayRecipeDetails();
                }
            }
            //if it doesn't match,will gv out a msg
            else
            {
                Console.WriteLine("There is no recipes found with the provided name.");
            }
        }

        // Method to scale a recipe
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

        // Method to reset the quantities of a recipe
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
                Console.WriteLine("Recipe not found.");
            }
        }
        // Method to clear all recipe data
        static void ClearData(List<Recipe> recipes)
        {
            recipes.Clear();
            Console.WriteLine("All recipe data has been cleared.");
        }
    }

    class Recipe
    {
        public string Name { get; }
        // Generic collection to store all ingredients for this recipe
        private List<Ingredient> ingredients = new List<Ingredient>();
        // Total calories for the recipe
        private int totalCalories; 

        public Recipe(string name)
        {
            Name = name;
        }

        // Method to get the total calories of the recipe
        public int GetTotalCalories()
        {
            int total = 0;
            foreach (var ingredient in ingredients)
            {
                //This process sums up the calories of all ingredients in the recipe.
                total += ingredient.Calories;
            }
            return total;                                                   
        }

        // Event for notifying the user when a recipe exceeds 300 calories
        // declares an event named NotifyCalorieExceedance of type RecipeNotification, which is a delegate
        public event RecipeNotification NotifyCalorieExceedance;

        // Method to add an ingredient to the recipe
        public void AddIngredient(string name, string quantity, string unit, int calories, string foodGroup)
        {
            ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            totalCalories += calories;

            //This conditional statement checks if the total calories of the recipe exceed 300.
            //If they do, it invokes the NotifyCalorieExceedance event, passing the name of the recipe (Name)
            //as an argument to any subscribed event handlers.
            if (totalCalories > 300)
            {
                NotifyCalorieExceedance?.Invoke(Name);
            }
        }

        // Method to display the details of the recipe
        public void DisplayRecipeDetails()
        {
            Console.WriteLine("\nRecipe: {0}", Name);
            Console.WriteLine("Ingredients:");
            //This loop iterates over each Ingredient object in the ingredients collection.
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine("{0}", ingredient);
            }

            // Set text color based on total calories
            if (totalCalories > 300)
            {
                //reseting the text color (RED) in the console to its default value after displaying the recipe details.
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("WARNING!! WARNING!! YOUR TOTAL CALORIES HAVE EXCEEDED 300");
            }
            else
            {
                //reseting the text color (GREEN) in the console to its default value after displaying the recipe details.
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Your Total Calories: {0}", totalCalories);
            }

            // Reset text color to default
            Console.ResetColor();
        }

        // Method to scale the recipe by a factor
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

        // Method to reset the quantities of the ingredients in the recipe
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

        // Private method to calculate the total calories of the recipe
        private int CalculateTotalCalories()
        {
            // initializing total to 0
            int total = 0;
            //foreach loop iterates over each ingredient in the ingredients collection.
            foreach (var ingredient in ingredients)
            {
                total += ingredient.Calories;
            }
            return total;
        }
    }

    class Ingredient
    {
        //property declarations
        //These properties are essential for encapsulating and managing the data related to an ingredient in the Ingredient class.
        //They provide controlled access to the class's fields, ensuring that certain properties can only be modified in specific ways
        public string Name { get; }
        public double Quantity { get; private set; }
        public string Unit { get; }
        public int Calories { get; }
        public string FoodGroup { get; }
        // Store original quantity for resetting
        private double originalQuantity; 

        public Ingredient(string name, string quantity, string unit, int calories, string foodGroup)
        {
            Name = name;
            //This line converts the quantity parameter to a double using Convert.ToDouble.
            Quantity = Convert.ToDouble(quantity);
            // Store original quantity
            originalQuantity = Quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }

        // Method to scale the quantity of the ingredient
        public void ScaleQuantity(double factor)
        {
            //This line multiplies the current value of Quantity by the provided factor and assigns the result back to Quantity.
            Quantity *= factor;
        }

        public void ResetQuantity()
        {
            // Reset quantity to original value
            Quantity = originalQuantity; 
        }

        // Override ToString method to display ingredient details
        public override string ToString()
        {
            //These are properties of the Ingredient class that hold the name, quantity, and unit of measurement of the ingredient.
            return string.Format("{0}: {1} {2}", Name, Quantity, Unit);
        }
    }
}
