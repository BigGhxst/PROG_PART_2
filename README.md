# PROG_PART_2

The program is a Recipe Manager application that allows users to manage recipes by inputting details, displaying, scaling, resetting, and clearing data.

## What is needed to compile and run this application
You can use Visual studio

### Usage
Once the program is running, you will be given with a menu for managing your recipes:
Menu Option
1.	Enter Recipe:
 prompts the user to enter the recipe's name.
 Allows the user to add several ingredients and specify the name, quantity, unit of measurement, calories, and food group of each.
 Adds the recipe to the recipe list.
2.	Display Recipes:
 It offers two options: view all recipes or search for a recipe by name.
 Displays the data of the recipes, including ingredients and total calories.
 To inform the user, the font colour changes to red if the total calories exceed 300, and the other is green when the calories are 300 and below.
3.	Scale Recipe:
 The user is prompted to enter the recipe's name to scale.
 Allows the user to specify a scaling factor (0.5, 2, or 3).
 Scales the quantities of all ingredients in the selected recipe and recalculates the total calories.
4.	Reset Quantities:
 To reset quantities, the user is prompted to input the recipe's name.
 Resets all component quantities to their original levels and recalculates total calories.
5.	Clear Data:
 Clears all recipe data from the application itself.
6.	Exit:
 Exit the application.

### GitHub repository link
https://github.com/Fortunemlilo/PROG_PART_2.git

### A brief description of what I changed based on the lecture's feedback.
The Recipe Manager application was significantly improved in response to the lecturer's feedback. The code is now better organized, with logical class definitions and clear, short comments that describe the logic and functionality. A Recipe class was established to contain recipe details, while an Ingredient class was built to manage specific ingredient features such as calories. This increased the readability and management of the code. The user interface was improved with a more interactive menu, and a search option was included to identify specific recipes by name. Calorie tracking was included for each item, along with a warning system that alerts users when total calories exceed 300. More importantly, ingredient quantity scaling and resetting are now more robust, with total calories recalculated following these operations. These modifications make the code more structured, easier to understand, and user-friendly.
