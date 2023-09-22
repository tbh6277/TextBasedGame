
namespace IGME206_TextBasedGame
{
    internal class Monkeys : Obstacle
    {
        private const int MaxMonkeys = 5;
        private const string ReactionQuestion = "How will you react?:";
        private static readonly string[] MonkeyChoices = { "Fight", "Hide", "Run" };

        internal new string successMessage = "\nCongratulations! You have defeated the monkeys!";
        internal new string defeatMessage = "\nOh no! The monkeys have defeated you.";
        
        private Random rand = new Random();
        private string monkeyString = "monkeys are";
        private DialogueHandler dh = new DialogueHandler();

        private int numMonkeys = 0;

        internal Monkeys() 
        {
            numMonkeys = rand.Next(1, MaxMonkeys);
            
            if (numMonkeys == 1)
            {
                monkeyString = "monkey is";
            }

            Console.WriteLine($"\nOh no! {numMonkeys} angry {monkeyString} running towards you!");

            Encounter();

            Failure();
        }


        internal override void Encounter()
        {
            int choice = dh.UserChoice(MonkeyChoices, ReactionQuestion);

            switch (choice)
            {
                case 0:
                    FightMonkeys();
                    break;
                case 1:
                    HideFromMonkeys();
                    break;
                default:
                    RunFromMonkeys();
                    break;
            }
        }

        private void FightMonkeys()
        {

        }

        private void HideFromMonkeys()
        {

        }

        private void RunFromMonkeys()
        {

        }
    }
}
