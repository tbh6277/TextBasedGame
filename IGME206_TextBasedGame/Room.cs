using System.Collections;

namespace IGME206_TextBasedGame
{
    internal class Room
    {
        private string roomType;
        private List<Room> connections = new List<Room>();
        private List<Obstacle> obstacles = new List<Obstacle>();
        private List<Item> items = new List<Item>();

        internal Room(string roomType)
        {
            this.roomType = roomType;
        }

        internal string RoomType { get { return roomType; } }

        internal void AddConnection(Room room)
        {
            if (!connections.Contains(room)) { this.connections.Add(room); }
        }

        internal void AddItem(Item item)
        {
            this.items.Add(item);
        }

        internal void AddObstacle(Obstacle obstacle)
        {
            this.obstacles.Add(obstacle);
        }

        private string[] ConnectionsAsStrings()
        {
            string[] stringConnections = new string[connections.Count];
            for (int i = 0; i < connections.Count; i++) 
            {
                stringConnections[i] = connections[i].RoomType;
            }

            return stringConnections;
        }

        internal Room LeaveRoom()
        {
            string exitString = "Which room would you like to go into?:";
            int choice = DialogueHandler.UserChoice(this.ConnectionsAsStrings(), exitString);
            return connections[choice];
        }

        public override bool Equals(object obj)
        {
            return obj != null && (obj as Room).roomType == this.roomType;
        }

        public override int GetHashCode()
        {
            return this.roomType.GetHashCode();
        }

        internal bool TryExitRoom(GhostHunter gh)
        {
            foreach (var o in obstacles)
            {
                if (o.ObstacleType == "Trap")
                { 
                    if (!o.ObstacleDefeated)
                    {
                        o.Encounter(gh);
                        return o.ObstacleDefeated;
                    }
                }
            }
            return true;
        }

        internal bool TryEnterRoom(GhostHunter gh)
        {
            foreach (var o in obstacles)
            {
                if (o.ObstacleType == "Lock" && !o.ObstacleDefeated )
                {
                    o.Encounter(gh);
                    return o.ObstacleDefeated;

                }
            }
            return true;
        }

        internal void RoomInteraction(GhostHunter gh)
        {
            List<int> interactionIndex = new List<int>();
            List<string> interactions = new List<string>();
            int obs = 10; // 1x, x = obstacle index, prereq: less than 10 interactive obstacles in room
            int its = 20; // 2x, x = items index

            foreach (var o in obstacles)
            {
                if (o.ObstacleType == "Ghost") 
                {
                    Console.WriteLine("\nA ghost stands in your path.");
                    interactions.Add($"Encounter the ghost of {(o as Ghost).Name}");
                    interactionIndex.Add(obs);
                    
                }
                obs += 1;
            }

            foreach (var i in items)
            {
                Console.WriteLine($"\nYou see something on the floor. It looks like a {i.ItemName}.");
                interactions.Add($"Pick up item: {i.ItemName}");
                interactionIndex.Add(its);
                its += 1;
            }

            if ( interactions.Count > 0 ) 
            {
                interactions.Add("View inventory.");
                interactions.Add("Do nothing in this room.");
                
                int choice = -1;

                while (interactions.Count > 2)
                {
                    choice = DialogueHandler.UserChoice(interactions.ToArray());
                    if (choice == (interactions.Count - 1)) {
                        break;
                    } 
                    else if (choice == (interactions.Count - 2))
                    {
                        gh.ViewInventory();
                    }
                    else
                    {
                        int interaction = interactionIndex[choice];
                        if (interaction >= 20)
                        {
                            interaction -= 20;
                            gh.PickupItem(items[interaction]);
                            interactions.RemoveAt(choice);
                            interactionIndex.RemoveAt(choice);
                            items.RemoveAt(interaction);
                        }
                        else
                        {
                            interaction -= 10;
                            obstacles[interaction].Encounter(gh);
                            if (obstacles[interaction].ObstacleDefeated)
                            {
                                interactions.RemoveAt(choice);
                                interactionIndex.RemoveAt(choice);
                            }
                        }
                    }
                } 
            }
            else
            {
                Console.WriteLine($"\nThe {this.roomType} appears to be empty.\n");
            }

        }


   
    }
}