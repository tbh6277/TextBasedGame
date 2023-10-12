
namespace IGME206_TextBasedGame
{
    internal class Obstacle
    {
        protected string successMessage;
        protected string defeatMessage;
        protected bool obstacleDefeated = false;
        protected string obstacleType;

        internal Obstacle (string successMessage, string defeatMessage, string obstacleType)
        {
            this.defeatMessage = defeatMessage;
            this.successMessage = successMessage;
            this.obstacleType = obstacleType;
        }

        internal string ObstacleType { get { return obstacleType; } }

        internal virtual void Encounter(GhostHunter gh) { }

        internal bool ObstacleDefeated { get { return this.obstacleDefeated; } }

        protected void Success()
        {
            Console.WriteLine(this.successMessage);
            obstacleDefeated=true;
        }


        protected virtual void Failure()
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
