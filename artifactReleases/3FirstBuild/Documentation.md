# Minimum Viable Product Documentation
  
Find the devlog log here: [src/BurnoutBuster/Devlog.md](https://github.com/IAMColumbia/gp2portfoliogame-CiarennHollis/blob/master/src/BurnoutBuster/Devlog.md)
  
## Progress Made
 * Bug Fixing from Vertical Slice
	* Character texture flash on hit
 	* Enemies colliding with each other
 	* Collision box/bounds not being positioned right on the player
	* Player hitbox being different from its collider for taking damage
 * Implemented multiple enemy types
 * Implemented another weapon type
	* Implemented weapons that can be picked up
 * Implemented game screens for the different screens of the games
 * Implemented having the enemies come in waves
  
## Sprint/Story Review
I got the majority of the work for this milestone done. The game is playable and there is a lose and a win. (Most) All of the GitHub Issues for this milestone have been close. The biggest hurdle for this milestone was getting the sword to move with the player. I tried to do so without giving the MonogameWeapon class a reference to the player but I couldn't get that to work no matter what I tried. Since I implemented the observer pattern for the ItemManager, I also implemented it for the weapon as well. So, the weapon does depend on the player for it's location when the player is moving. Plus it also allows for the game to have weapons in its scene who locations aren't dependent on the player. I had wanted to also implement health pick ups, but that ended up being out of scop for this project. Though, the structure is there for implementing it later through the item manager.
Overall, this was a successful sprint. The game doesn't have the best game feel to it, but I think that is mainly because I don't have animations implemented. I left animations for last since I wanted to make sure all the main functionality worked before making it look and feel nice. The next sprint will focus on bugfixing and animations. 
  
## Structure
I like thinking of the pieces of this project as building blocks and I tried my best to have the structure of the game/project be like that. Several things can be plugged and uplugged from each other be it objects (like MonogameEnemy.cs). Some things, of course, depend on other things so there are some things that can't be plugged in, but they aren't many. :P
 * See the for the structure: (artifactReleases\3FirstBuild\BBMVPClassDiagram.png)[https://github.com/IAMColumbia/gp2portfoliogame-CiarennHollis/blob/MVP/artifactReleases/3FirstBuild/BBMVPClassDiagram.png]
  
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
Most of the things/objects in the project are rather stateful. I tend to use states for toggles as well as for noting the status of the moving pieces in the game. For example, both the player and enemies have state to note their status and the game uses and reacts to that status.There are a few odd places where a boolean is used for state, but that is only if there are 2 states (ie. the MonogameWeapon has a isHeld var and updates itself based off the value of that boolean. With this, these is only two states ever (being held, not being held) so I used boolean for it) There are other places in the project where there are enums for state that only have one or two values since I made those with the intention of expanding them.
  
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
 * Observer -> allows some gameComponents to listens and react to when the properties they're interested in change which out them having to have a hard coded reference to that thing
  
## Not Working
 * Enemy manager gets rid of the enemies that are still active in the game when it spawns in new ones
 * The original sword the player starts with doesn't go away after it's been replaced by the new one when the player picks it up (it's visbile behind the new sword)
  
## Didn't Get to Implement
 * Game Animations
 * Game Music
 * Health Pick Ups [the system is kind of there for it, it wouldn't be hard to implement]
