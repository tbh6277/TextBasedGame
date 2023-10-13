NOTE: If you are running via the Visual Studio debugger instead of the command line, 
the filepath constants CASTLE_MAP_PATH and CASTLE_SETUP_PATH need to be adjusted 
for working directory. The necessary change in the Setup.cs file should be:

- CASTLE_MAP_PATH = "../../../Resources/CastleMap.txt"
- CASTLE_SETUP_PATH = "../../../Resources/CastleSetup.txt"
- ITEMS_LIST_PATH = "../../../Resources/ItemsList.txt"
- GHOST_LIST_PATH = "../../../Resources/GostList.txt"
- GHOST_ITEMS_PATH = "../../../Resources/GostItems.txt"
- LOCKED_DOORS_PATH = "../../../Resources/LockedDoors.txt"
- TRAPS_LIST_PATH = "../../../Resources/TrapList.txt"

NOTE #2: Upon submission, I forgot to remove the line in the Ghost constructor that set the health to 1 for testing purposes. It is fixed here in the github but not in the actual submission.

For grading, the things to look at are:

Classes Inhereted from Obstacle:
- Gost
- Trap
- Lock
- These are all obstacles that result in either a success or failure

Derived Class Override + extra member variable:
- Ghost: name -> the ghost has a name, other obstacles do not
- Lock: overrides Failure() because a failure to unlock does not mean defeat

Polymorphic behavior
- (I am assuming this is referring to subtype polymorphism)
- In the Ghost class, it has a list of obstacles that may be any of the child types

Overloading + loop:
- Program.RunGame()
	- (The reason it is polymorphic is because it is also called from Obstacle.Failure() in the event that the user wishes to try again)

Recursion:
- In ghost hunter -> printing the list of items

Take user input into account to make choices:
- Easiest example is in the while loop of Program.RunGame()
- DialogueHandler.Choices() handles the actual interaction with the user

Take user input in accounts to make choices 
	can choose directions to progress to
	can choose action to fight entity in room

Static member variable:
- in program, list of user choices


When battle won -> should win item stored in array or list
give user option to use an item from stack to win battle or unlock door
upon exit or @ any point upon request, list remaining items in stack
