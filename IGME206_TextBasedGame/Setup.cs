using System.IO;
using System.Threading.Tasks;

namespace IGME206_TextBasedGame
{
	internal class Setup
	{
        private const string CASTLE_MAP_PATH = "./Resources/CastleMap.txt";
        private const string CASTLE_SETUP_PATH = "./Resources/CastleSetup.txt";
        private const string ITEMS_LIST_PATH = "./Resources/ItemsList.txt";
        private const string GHOST_LIST_PATH = "./Resources/GhostList.txt";
        private const string GHOST_ITEMS_PATH = "./Resources/GhostItems.txt";
        private const string LOCKED_DOORS_PATH = "./Resources/LockedDoors.txt";
        private const string TRAPS_LIST_PATH = "./Resources/TrapList.txt";

        private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        private Dictionary<string, Item> items = new Dictionary<string, Item>();
        private Dictionary<string, Ghost> ghosts = new Dictionary<string, Ghost>();

        internal string SetupMap()
        {
            string map = "";

            try
            {
                map = File.ReadAllText(CASTLE_MAP_PATH);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Could not read {CASTLE_MAP_PATH}: {e}");
            }

            return map;
        }

        private void ItemSetup()
        {
            try
            {
                Parallel.ForEach (File.ReadLines(ITEMS_LIST_PATH), line => 
                {
                    string[] itemLine = line.Split(';');
                    string rName = itemLine[0];
                    string[] item = itemLine[1].Split(':');
                    string iName = item[0];

                    string description = "";
                    if (item.Length > 1)
                    {
                        description = item[1];
                    }

                    if (rooms.ContainsKey(rName) && !items.ContainsKey(iName))
                    {
                        Room room = rooms[rName];                     
                        Item i = new Item(iName, description);
                        room.AddItem(i);
                        items.Add(iName, i);
                        
                    }
                });
            }
            catch (Exception e) 
            {
                Console.Error.WriteLine("Problem setting up items: " + e);
            }
        }

        private void ObstacleSetup()
        {
            try
            {
                // init locked doors and keys
                Parallel.ForEach(File.ReadLines(LOCKED_DOORS_PATH), line =>
                {
                    string[] lockLine = line.Split(';');
                    string rName = lockLine[0];
                    string key = lockLine[1];

                    if (rooms.ContainsKey(rName) && items.ContainsKey(key))
                    {
                        Room r = rooms[rName];
                        Item k = items[key];

                        Lock l = new Lock(r.RoomType, k);
                        r.AddObstacle(l);
                    }
                });

                // init traps
                Parallel.ForEach(File.ReadLines(TRAPS_LIST_PATH), line =>
                {
                    string[] trapLine = line.Split(';');
                    string rName = trapLine[0];
                    string trap = trapLine[1];
                    string escapeItem = trapLine[2];

                    if (rooms.ContainsKey(rName) && items.ContainsKey(escapeItem))
                    {
                        Room r = rooms[rName];
                        Item i = items[escapeItem];

                        Trap t = new Trap(r.RoomType, trap, i);
                        r.AddObstacle(t);
                    }
                });


                // init ghosts
                foreach (string line in File.ReadLines(GHOST_LIST_PATH))
                {
                    string[] ghostLine = line.Split(';');
                    string rName = ghostLine[0];
                    string gName = ghostLine[1];
                    string description = ghostLine[2];
                    string cause = ghostLine[3];
                    string? personResponsible = null;
                    string? weapon = null;
                    if (ghostLine.Length > 4)
                    {
                        personResponsible = ghostLine[4];  
                    }
                    if (ghostLine.Length > 5)
                    {
                        weapon = ghostLine[5];
                    }

                    CauseOfDeath gCOD = new CauseOfDeath(cause, description);

                    if (personResponsible != null && ghosts.ContainsKey(personResponsible))
                    {
                        gCOD.PersonResponsible = ghosts[personResponsible];
                    }

                    if (weapon != null && items.ContainsKey(weapon))
                    {
                        gCOD.Weapon = items[weapon];
                    }

                    Ghost g = new Ghost(gName, gCOD);
                    ghosts.Add(gName, g);

                    if (rooms.ContainsKey(rName))
                    {
                        Room r = rooms[rName];
                        r.AddObstacle(g);
                    }
                }

                // assign items to ghosts
                Parallel.ForEach (File.ReadLines(GHOST_LIST_PATH), line => 
                {
                    string[] itemLine = line.Split(';');
                    string gName = itemLine[0];
                    string iName = itemLine[1];

                    if (ghosts.ContainsKey(gName) && items.ContainsKey(iName))
                    {
                        Ghost g = ghosts[gName];
                        Item i = items[iName];

                        Console.WriteLine(gName +" - " + iName);
                        i.Owner = g;
                        g.AddItem(i);
                    }
                });

            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Problem setting up obstacles: " + e);
            }

        }

        internal Room CastleSetup(string startRoom)
        {
            Room startingRoom = new Room("Empty Room");

            try
            {
                foreach (string line in File.ReadLines(CASTLE_SETUP_PATH))
                {
                    string[] roomNames = line.Split(';');
                    string rName = roomNames[0];
                    
                    if (!rooms.ContainsKey(rName))
                    {
                        rooms.Add(rName, new Room(rName));
                    }
                    
                    Room curr = rooms[rName];

                    foreach (string name in roomNames[1..])
                    {
                        if(!rooms.ContainsKey(name))
                        {
                            rooms.Add(name, new Room(name));
                        } 
                        curr.AddConnection(rooms[name]);
                        rooms[name].AddConnection(curr);
                    }
                }
            } catch (Exception e)
            {
                Console.Error.WriteLine("Problem setting up castle: " + e);
            }
            
            if(rooms.ContainsKey(startRoom))
            {
                startingRoom = rooms[startRoom];
            }

            ItemSetup();
            ObstacleSetup();

            return startingRoom;


        }
    }
}
