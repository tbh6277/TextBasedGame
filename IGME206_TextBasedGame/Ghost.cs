using System.Collections;

namespace IGME206_TextBasedGame
{
    internal class Ghost : Obstacle
    {
        private static string[] ghostChoices = { "Attack ghost",
            "Ask ghost who they are and how they died", "View Inventory", "Come back later" };
        private static string[] ghostAttacks = { " haunts you.", " tries to possess you.", " drains your lifeforce" };
        private string name = "";
        private CauseOfDeath? causeOfDeath;
        private List<Item> items = new List<Item>();
        private List<bool> itemUsed = new List<bool>();
        private int health;
        private int angerLevel = 0;
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
            this.health = 1;
        }

        internal string Name
        {
            get { return name; }
        }

        internal void AddItem(Item item)
        {
            this.items.Add(item);
            this.itemUsed.Add(false);
        }

        internal bool ItemImportance(Item item) {

            int index = -1;
            if(this.items != null && this.items.Contains(item))
            {
                index = this.items.IndexOf(item);
            }

            if(index != -1 && !this.itemUsed[index])
            {
                Console.WriteLine("\nThis " + item.ItemName + " has meaning to this ghost. Their health is cut in half.");
                this.itemUsed[index] = true;
                return this.TakeDamage(health / 2);
            } 
            else if (index != -1)
            {
                Console.WriteLine("\nThis item has already been used to weaken the ghost of " + name + ". It cannot be used again.");
            }
            else if (causeOfDeath.Weapon != null && item == causeOfDeath.Weapon && causeOfDeath.Cause == "murder")
            {
                Console.WriteLine("\nYou brandish the murder weapon that killed this ghost. This engrages " +
                    "them and now they can do up to 10 points more damage.");
                maxDamage += 10;
            }
            else
            {
                Console.WriteLine("\nThis item means nothing to the ghost of " + name);
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
                Console.WriteLine("Their health is now at " + health + ".");
                return false;
            }
        }

        private bool Attack(GhostHunter gh)
        {
            Console.WriteLine("\nThe ghost of " +  this.name + ghostAttacks[rnd.Next(0,3)]);
            int damage = rnd.Next(maxDamage);
            return gh.TakeDamage(damage);
        }

        private void Fight(GhostHunter gh, bool ghTurn)  
        {
            bool ghFlee = false;

            while (this.health >= 0 && !ghFlee)
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
                Console.WriteLine("\nYou call out to the ghost, 'Who are you, lost spirt? " +
                    "How did you come to be haunting these halls?'");
                if ( angerLevel > 3)
                {
                    Console.WriteLine("\nThis ghost is angry. Your question provokes them and they attack.");
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

