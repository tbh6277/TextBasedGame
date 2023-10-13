namespace IGME206_TextBasedGame
{
    internal class Trap : Obstacle
    {
        private static string[] trapChoices = { "Try and use an inventory item to escape",
            "View inventory", "Give up" };
        private string roomName;
        private string trapName;
        private Item key;
        private bool fallen = false;
        private const int MAX_FALL_DAMAGE = 10;

        private new const string successMessage = "";
        private new const string defeatMessage = "\nYou have perished.";
        private const string OBSTACLE_TYPE = "Trap";

        internal Trap(string roomName, string trapName, Item key) 
            : base (successMessage, defeatMessage, OBSTACLE_TYPE)
        {
            this.roomName = roomName;
            this.trapName = trapName;
            this.key = key;

        }

        internal override void Encounter(GhostHunter gh)
        {
            if (!fallen)
            {
                Console.WriteLine("Oh no! The stairs have caved in and you have fallen. You are now trapped.");
                gh.TakeDamage(rnd.Next(MAX_FALL_DAMAGE));
                fallen = true;
            }
            
            int choice = DialogueHandler.UserChoice(trapChoices, "\nHow will you try and escape the " + this.roomName + ": ");
            if(choice == 0)
            {
                Item c = gh.SelectItem();
                if (c.ItemName == "coil of rope")
                {
                    Console.WriteLine("\nYou tie a large knot in your rope and throw it into the splintered " +
                        "floorboards above. It catches inbetween the boards and you pull it taught. It appears" +
                        " that it will hold your weight. You can now escape the " + roomName);
                    this.Success();
                }
                else
                {
                    Console.WriteLine("Unfortunately, " + c.ItemName + " cannot help you. Try something else.");
                    this.Encounter(gh);
                }
            }
            else if(choice == 1)
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