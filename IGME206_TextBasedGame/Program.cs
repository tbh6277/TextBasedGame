namespace IGME206_TextBasedGame
{
    internal class Program
    {

        internal static void RunGame()
        {
            DialogueHandler dh = new DialogueHandler();

            int choice = dh.UserChoice(dh.testChoices);
            Console.Write("You chose " + dh.testChoices[choice]);

            new Monkeys();
        }


        public static void Main(string[] args)
        {
            RunGame();
        }
    }
}
