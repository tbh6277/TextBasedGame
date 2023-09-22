
namespace IGME206_TextBasedGame
{
    internal abstract class Obstacle
    {
        protected string successMessage = "You have won!";
        protected string defeatMessage = "You have lost.";
        internal abstract void Encounter();

        protected void Success()
        {
            Console.WriteLine(successMessage);
        }


        public void Failure()
        {
            Console.WriteLine("\n" + this.defeatMessage);
            Console.WriteLine("Would you like to try again? y/n");
            
            string? input = Console.ReadLine();
            char choice; 
            while (!char.TryParse(input, out choice) && choice != 'y' && choice != 'n')
            {

                Console.WriteLine($"Sorry, '{choice}' is not a valid choice. Please try another.");
                input = Console.ReadLine();
            }

            if (choice == 'y')
            {
                Program.RunGame();
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
                Environment.Exit(0);
            }

        }
    }
}
