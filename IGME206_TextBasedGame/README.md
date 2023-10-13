NOTE: If you are running via the Visual Studio debugger instead of the command line 
(via 'dotnet run'), the filepath constants need to be adjusted for the working directory. 

The necessary change in the Setup.cs file should be:

CASTLE_MAP_PATH = "../../../Resources/CastleMap.txt"
CASTLE_SETUP_PATH = "../../../Resources/CastleSetup.txt"
ITEMS_LIST_PATH = "../../../Resources/ItemsList.txt";
GHOST_LIST_PATH = "../../../Resources/GostList.txt";
GHOST_ITEMS_PATH = "../../../Resources/GostItems.txt";
LOCKED_DOORS_PATH = "../../../Resources/LockedDoors.txt";
TRAPS_LIST_PATH = "../../../Resources/TrapList.txt";


For grading, the things to look at are:

5 classes:
1. Room -> rooms are stored in graph via Room[] connections
2. Obstacle
3. Ghost (enemy to fight)
4. Lock
5. GhostHunter

Classes Inhereted from Obstacle:
- Gost
- Lock
- Trap

Static member variable:
- in program, list of user choices

Polymorphic behavior
- (I am assuming this is referring to subtype polymorphism)
- In the Ghost class, it has a list of obstacles that may be any of the child types

Recursion:
- In ghost hunter -> printing the list of items
- (Also in Ghost.Encounter(), but that one is less straightforward)

Loop:
- Program.RunGame()
- Also basically every class has one

Overloaded method:
- Program.RunGame()
- Program.RunGame(startRoom)

Derived Class Override + extra member variable:
- Ghost: name -> the ghost has a name, other obstacles do not
- Lock: overrides Failure() because a failure to unlock does not mean defeat

The derived class attributes and members should be clear in Obstacle, Ghost, or Lock

Item is "loot" class. They can be picked up and used as needed.
