
namespace IGME206_TextBasedGame
{
	internal class GhostHunter
	{
		private int health = 100;
		private List<Item> inventory = new List<Item> { };
		private int soulCount = 0;
		private int maxDamage = 16;
		private Random rnd = new Random();
		private static string[] ATTACK_CHOICES = {"Attack with vacuum", "Use item from inventory",
			"Use a Soul Crystal to better understand the ghost", "Flee"};

		internal GhostHunter() {}

		internal int SoulCount { get { return soulCount; } }

		internal void ViewInventory()
		{
			if (inventory.Count == 0) 
			{
				Console.WriteLine("\nYour inventory is empty.\n");
			}
			else
			{
				RecursivePrintInventory(0);
			}
		}

		internal Item? ChooseKey() 
		{
			List<string> keys = new List<string>();
			List<int> keyIndex = new List<int>();

			int ki = 0;
			foreach (var i in inventory)
			{
				if(i.ItemName.Contains("key"))
				{
					keys.Add(i.ItemName);
					keyIndex.Add(ki);
				}
				ki ++;
			}

			if (keys.Count > 0 )
			{
				int choice = DialogueHandler.UserChoice(keys.ToArray(), "\nChoose a key to try: ");
				return inventory[keyIndex[choice]];
			}
			else
			{
				Console.WriteLine("\nIt appears that you have no keys in your inventory. Try searching" +
					" for one and come back later.");
				return null;
			}
		}

		internal Item SelectItem()
		{
			string[] items = new string[inventory.Count];
			for(int i = 0; i <  inventory.Count; i++)
			{
				items[i] = inventory[i].ItemName;
			}

			int choice = DialogueHandler.UserChoice(items, "\nPlease select an item: ");
			return inventory[choice];
		}

		internal bool Attack(Ghost g)
		{
			int choice = DialogueHandler.UserChoice(ATTACK_CHOICES);

			if (choice == 0)
			{
                int damage = rnd.Next(maxDamage);
                g.TakeDamage(damage);
            }
			else if (choice == 1)
			{
				Item i = SelectItem();
				g.ItemImportance(i);
			}
			else if (choice == 2)
			{
				g.QuestionGhost();
			} 
			else if (choice == 3)
			{
				return true;
			}
			return false;
		}

		internal bool TakeDamage(int damage)
		{
			health -= damage;
			if (health < 0)
			{
				Console.WriteLine("\nYour health has dropped below zero, so now you will join the ghosts forever.");
				return true; // returns true if you die
			}
			else
			{
				Console.WriteLine("\nYou have taken " +  damage + " damage.");
				Console.WriteLine("\nYour health is now at " + health + ".");
				return false;
			} 
		}

		internal void PickupItem(Item item)
		{
			inventory.Add(item);
			Console.WriteLine($"\n'{item.ItemName}' has been added to your inventory." +
				$"\nView in your inventory to learn more.\n");
		}

		internal void CollectGhost(Ghost g)
		{
			Console.WriteLine("You now possess the ghost of " + g.Name);
			soulCount += 1;
		}

		private void RecursivePrintInventory(int i)
		{
			if (i < inventory.Count)
			{
				Console.WriteLine($"\n{i + 1}: {inventory[i].ToString()}");
				RecursivePrintInventory(i + 1);
			} 
			else { Console.WriteLine("\n");  }
        }
	}
}
