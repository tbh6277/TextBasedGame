namespace IGME206_TextBasedGame
{
    internal class Lock : Obstacle
    {
        private static string[] lockChoices = { "Try and unlock the door", 
            "Try and break the door in", "View Inventory", "Come back later" };
        private string roomName;
        private Item key;
        private const int MAX_DOOR_BREAK_DAMAGE = 5;

        private new const string successMessage = "\nSuccess! You have unlocked the door!";
        private new const string defeatMessage = "\nThe door remains locked. You are unable to enter the room.";
        private const string OBSTACLE_TYPE = "Lock";

        internal Lock(string roomName, Item key)
            : base(successMessage, defeatMessage, OBSTACLE_TYPE)
        {
            this.roomName = roomName;
            this.key = key;
        }

        protected override void Failure()
        {
            Console.WriteLine(defeatMessage);
        }

        internal override void Encounter(GhostHunter gh)
        {
            Console.WriteLine($"\nThe door to the {this.roomName} is locked.");

            int choice = DialogueHandler.UserChoice(lockChoices);

            if (choice == 0)
            {
                Item? k = gh.ChooseKey();
                if (k != null && k == this.key)
                {
                    this.obstacleDefeated = true;
                    this.Success();
                }
                else
                {
                    this.Failure();
                }
            }
            else if (choice == 1)
            {
                bool perish = gh.TakeDamage(rnd.Next(MAX_DOOR_BREAK_DAMAGE));
                // This option exists because I think its funny
                if (rnd.Next(1000) == 1)
                {
                    Console.WriteLine("\nWith incredible luck, the old door gives way as you slam into it.");
                    this.Success();

                    if (perish) { base.Failure(); }
                }
                else
                {
                    Console.WriteLine("\nYou slam into the door, but it doesn't budge.");
                    if ( perish )
                    {
                        base.Failure();
                    }
                    else
                    {
                        this.Failure();
                    }
                }

            }
            else if (choice == 2)
            {
                gh.ViewInventory();
                this.Encounter(gh);
            }
            else
            {
                this.Failure();
            }
            

        }

    }
}

