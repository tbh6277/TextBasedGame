
namespace IGME206_TextBasedGame
{
    internal static class DialogueHandler
    {

        /* Handle when the user must choose from a list of options
		 * Provides user with a list of options and processes the result
		 * 
		 * param: choices -> array of options 
		 * param: question -> dialogue presenting choices to user 
		 * return: index of the selected choice
		 */
        internal static int UserChoice(
            string[] choices,
            string question = "\nPlease select one of the following actions:")
        {
            // Presents choices in form of 'Please select one of the following: (1, 2, 3)'
            Console.WriteLine($"{question} ({OptionsString(choices.Length)})");

            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"\t{i + 1}: {choices[i]}");
            }

            int choice = -1;
            Console.Write("> ");
            string? input = Console.ReadLine();

            // If user input is not a valid choice, loop until valid
            while (!int.TryParse(input, out choice) || choice < 1 || choice > choices.Length)
            {
                if (input == null) { input = " "; }

                Console.WriteLine($"Sorry, '{input}' is not a valid choice. Please try another.");
                Console.Write("> ");
                input = Console.ReadLine();
            }

            return choice - 1; // choices start from 1

        }

        internal static void Wait()
        {
            Console.Write("\n press enter to continue > ");
            Console.ReadLine();
        }

        // return: list of numbers in form of '1, 2, 3'
        private static string OptionsString(int i)
        {
            string options = "";
            for (int j = 1; j < i; j++)
            {
                options += j + ", ";
            }
            return options += i;
        }
    }
}
