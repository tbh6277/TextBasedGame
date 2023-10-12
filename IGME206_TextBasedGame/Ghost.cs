using System.Collections;

namespace IGME206_TextBasedGame
{
    internal class Ghost : Obstacle
    {
        private static string[] ghostChoices = { "Attack ghost",
            "Ask ghost who they are and how they died", "View Inventory", "Come back later" };
        private string name = "";
        private CauseOfDeath? causeOfDeath;
        private List<Item>? items;
        private int health;
        private int angerLevel = 0;
        private Random rnd = new Random();
        private int maxDamage;

        private new const string successMessage = "\nYou have defeated the ghost";
        private new const string defeatMessage = "\nYou have been defeated by the ghost";
        private const string OBSTACLE_TYPE = "Ghost";

        internal Ghost(string name, CauseOfDeath causeOfDeath)
            : base(successMessage, defeatMessage, OBSTACLE_TYPE)
        {
            this.name = name;
            this.causeOfDeath = causeOfDeath;
            this.angerLevel = causeOfDeath.CauseIndex;
            this.maxDamage = (angerLevel + 1) * 3 + 1;
            this.health = (angerLevel + 1) * 5;
        }

        internal string Name
        {
            get { return name; }
        }

        internal void AddItem(Item item)
        {
            this.items.Add(item);
        }

        internal bool ItemImportance(Item item) {
            if(this.items != null && this.items.Contains(item))
            {
                Console.WriteLine("\nThis " + item.ItemName + " has meaning to this ghost.");
                return this.TakeDamage(rnd.Next(5, maxDamage));
            }
            return false;
        }

        internal bool TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                Console.WriteLine("\nThe ghost of " + name + " has been defeated."); 
                return true; // returns true if the ghost has been defeated
            }
            else
            {
                Console.WriteLine("\nThe ghost of " + name + " has taken " + damage + " damage.");
                Console.WriteLine("\nTheir health is now at " + health + ".");
                return false;
            }
        }

        private bool Attack(GhostHunter gh)
        {
            Console.WriteLine("\nThe ghost of " +  this.name + " tries to suck the life out of you.");
            int damage = rnd.Next(maxDamage);
            return gh.TakeDamage(damage);
        }

        private void Fight(GhostHunter gh, bool ghTurn)  
        {
            bool ghFlee = false;

            while (this.health > 0 && !ghFlee)
            {
                if (ghTurn)
                {
                    ghFlee = gh.Attack(this);
                    ghTurn = !ghTurn;
                    DialogueHandler.Wait();
                }
                else
                {
                    bool ghDefeated = this.Attack(gh);
                    if (ghDefeated)
                    {
                        this.Failure();
                    }
                    ghTurn = !ghTurn;
                    DialogueHandler.Wait();
                }
            }
            if (this.health < 0)
            {
                gh.CollectGhost(this);
                this.Success();
            }

        }
        
        internal void QuestionGhost()
        {
            Console.WriteLine("\nA flood of images wash over you and you understand that they are the " +
                    "the ghost of " + name + ". " + name + this.causeOfDeath.Description);
        }

        internal override void Encounter(GhostHunter gh)
        {
            int choice = DialogueHandler.UserChoice(ghostChoices);

            if (choice == 0)
            {
                Fight(gh, true);
            }
            else if (choice == 1)
            {
                Console.WriteLine("You call out to the ghost, 'Who are you, lost spirt? " +
                    "How did you haunting these halls?'");
                if ( angerLevel > 3)
                {
                    Console.WriteLine("The ghost is an angry one. Your question provokes them and they attack.");
                    Fight(gh, false);
                }
                else
                {
                    this.QuestionGhost();
                    this.Encounter(gh);
                }                
            }
            else if (choice == 2)
            {
                gh.ViewInventory();
                this.Encounter(gh);
            }
        }
    }
}

