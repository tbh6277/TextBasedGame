
namespace IGME206_TextBasedGame
{
    internal class Program
    {
        private static string[] choices = new string[] { "Interact with Room", "Continue Exploring", "View Map", "View Inventory", "View Stats", "Quit Game" };
        private const string choiceString = "\nWhat would you like to do next?:";
        private const string START_ROOM = "Outer Gate";
        private static string castleMap = "";
        private static Room? startRoom;
        private static int NUM_OF_GHOSTS = 13;
        private static GhostHunter gh = new GhostHunter();

        private static Room SetupGame()
        {
            Setup setup = new Setup();
            castleMap = setup.SetupMap();
            Room startRoom = setup.CastleSetup(START_ROOM);
            return startRoom;
        }

        internal static void RunGame()
        {
            RunGame(startRoom);
        }

        internal static void EndGame()
        {
            Console.WriteLine("\nYour final inventory is: ");
            gh.ViewInventory();
            Console.WriteLine("Thanks for playing!");
            Environment.Exit(0);
        }

        private static void RunGame(Room currRoom)
        {

            Console.WriteLine("\nYou stand in the " + currRoom.RoomType + ".");
            int choice;

            do
            {
                choice = DialogueHandler.UserChoice(choices, choiceString);
                Console.WriteLine("\nYou stand in the " + currRoom.RoomType + ".");
                if (choice == 0)
                {
                    currRoom.RoomInteraction(gh);
                }
                else if (choice == 1)
                {
                    if (currRoom.TryExitRoom(gh))
                    {
                        Room newRoom = currRoom.LeaveRoom();
                        if (newRoom.TryEnterRoom(gh))
                        {
                            currRoom = newRoom;
                            currRoom.RoomInteraction(gh);
                        }
                        else
                        {
                            Console.WriteLine("\nYou stand in the " + currRoom.RoomType + ".");
                        }
                    }

                }
                else if (choice == 2)
                {
                    Console.WriteLine(castleMap);
                }
                else if (choice == 3)
                {
                    gh.ViewInventory();
                }
                else if (choice == 4)
                {
                    gh.ViewStats();
                }

            } while (choice != 5 && gh.SoulCount < 13);

            if(gh.SoulCount >= 13)
            {
                Console.WriteLine("\nCongratulations, investigator! You have captured all of the ghosts.");
                DialogueHandler.Wait();
                Console.WriteLine("\nWith your mission complete, you take the ghosts outside the castle " +
                    "and free them into the afterlife.");
                DialogueHandler.Wait();
            } 

            EndGame();
        }

        public static void Main(string[] args)
        {
            startRoom = SetupGame();

            Console.WriteLine("\nYou stand at the gates of Windheld Keep, a castle abandoned over a hundred " +
                "years ago when tales of strange happenings led to the people fleeing for fear " +
                "of it being haunted. Since then, it was boarded up and closed off. Many have " +
                "snuck in over the years to try and disprove the legends, but none have returned. ");

            DialogueHandler.Wait();
            
            Console.WriteLine("\nThe local authorities have hired you, a reknowed paranormal investegator, " +
                "following yet another disappearance in order to determine the fate of these " +
                "unlucky souls, or, if the legends are true and the castle is haunted, to clear " +
                "it's halls of its otherworldly inhabitants. ");

            DialogueHandler.Wait();

            Console.WriteLine("\n" + castleMap + "\n");
            Console.WriteLine("\nBefore sending you off, the authorities gave you this old map from the library " +
                "and final warning before you enter:" +
                "\nBe just as wary of traps as waywards souls. It is an old castle, and no one " +
                "knows what state of disarray it is in.");

            DialogueHandler.Wait();

            Console.WriteLine("\nI wish you luck, investigator! You will need it.");

            DialogueHandler.Wait();

            RunGame();

            Console.WriteLine("\nThanks for playing!");
        }
    }
}
