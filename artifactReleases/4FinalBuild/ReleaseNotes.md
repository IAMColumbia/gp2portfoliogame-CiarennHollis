# Final Product Documentation
  
Find the devlog log here: [src/BurnoutBuster/Devlog.md](https://github.com/IAMColumbia/gp2portfoliogame-CiarennHollis/blob/master/src/BurnoutBuster/Devlog.md)
  

See the [README file](https://github.com/IAMColumbia/gp2portfoliogame-CiarennHollis/blob/master/README.md) for project details (genre, platform, etc.)
  
Find the devlog log here: [src/BurnoutBuster/Devlog.md](https://github.com/IAMColumbia/gp2portfoliogame-CiarennHollis/blob/master/src/BurnoutBuster/Devlog.md)
  
## Progress Made
 * Created and implemented character animations
 * Creates and implemented weapon animations
 * Balanced the gameplay mathematically
 * Made sure debugging tools don't get baked into the release build
 * Fixed typos
 * Found and implemented game music
 * Bug fixing the enemy manager
  
## Sprint/Story Review
This sprint was mostly focused on polishing up the game since it was already at its minimum viable product state. That polish bug-fixing but the majority of the time was spent creating and implementing animations into the game. That took longer than I thought it would and so other things got pushed aside because of that. The animations aren't really implemented the way I wanted them to be and I don't really have the bandwidth anymore to figure out why. I hit some constraints with the way I implemented the observer pattern in this project, it's not really the most flexible and makes it so that a subject's observers really only observe on thing or one change.  
  
## Structure
I like thinking of the pieces of this project as building blocks and I tried my best to have the structure of the game/project be like that. Several things can be plugged and uplugged from each other be it objects (like MonogameEnemy.cs). Some things, of course, depend on other things so there are some things that can't be plugged in, but they aren't many. :P
Some parts of the structure are MonoGame dependent, but those dependencies are mostly wrappers for plain C# classes that define the components of the game without needing MonoGame. More on this in dependencies.
 * See the for the structure: [artifactReleases\4FinalBuild\BBMVPClassDiagram.png](https://github.com/IAMColumbia/gp2portfoliogame-CiarennHollis/blob/MVP/artifactReleases/4FinalBuild/BBMVPClassDiagram.png).
  
## Dependencies
 * I tried my best to have things been relatively clean and only giving them dependencies when needed. Some are obvious (like the enemies depend on the enemy manager) but most of the other are rather deliberate.
 * There also are some magic numbers around, most of them are noted with a comment
### Dependency list 
 * Enemies depend on the creature/player for movement
 * Enemy manager has enemies
 * Collisin handler depends on all the collidable objects in the game
 * All the collidable objects depend on the collision handler
 * Command processor depends on the timed input handler 
 * creature/player depends on the command processor for movement and actions
 * Command processor has a chord analyzer 
 * EnemyManager notifies its observers (itemManager)
 * MonogameCreature notfies its observers (a MonogameWeapon)
 * Interactable objects depend on the collision handler
 * MonogameWeapons have a weapon encapsulated in them
 * MonogameEnemies have an enemy encapsulated in them
 * MonogameCreature has a creature encapsulated in it
  
## State
Most of the things/objects in the project are rather stateful. I tend to use states for toggles as well as for noting the status of the moving pieces in the game. For example, both the player and enemies have state to note their status and the game uses and reacts to that status. There are a few odd places where a boolean is used for state, but that is only if there are 2 states (ie. the MonogameWeapon has a isHeld var and updates itself based off the value of that boolean. With this, these are only two states ever (being held, not being held) so I used boolean for it) There are other places in the project where there are enums for state that only have one or two values since I made those with the intention of expanding them.
  
## Systems
 * Collision System -> handles collision
 * Command Processor and Input -> input buffer and chord analyzer as well as timers 
 * Enemy Manager -> manages the enemies
 * Level Manager -> intended to load and manage the levels of the game
 * Item Manager -> manages the item(s) in the game 
 * HUD -> manages displaying the needed values for the player: health, wave number, etc
 * Game Console -> debugging 
  
## Patterns Used
 * Strategy -> enemies, weapons
 * Command -> input -> player movement and actions 
 * (pseudo) Object pool -> enemies (pseudo because it's not implementing an Object Pool class like previous demos we've looked at)
 * Singleton -> tag manager 
 * (sort of) Fly Weight -> commands get passed between the chord analyzer and command processor via an enum value rather than actually passing commands through
 * Observer -> allows some gameComponents to listen and react to when the properties they're interested in change without them having to have a hard-coded reference to that thing
  
## Not Working
 * Multiple sword swing animations -> thus there is no visual difference between a basic attack and a heavy attack
  
## Didn't Get to Implement
 * Health Pick-Ups [the system wasn't as flexible as I thought, I didn't put enough thought into the item manager in order to implement this smoothly. As a result, the item is implemented, the system for spawning them isn't]
 * Sound effects
 * Unique art for the different screens (lose, win)
