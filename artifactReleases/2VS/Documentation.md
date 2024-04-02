# Vertical Slice Documentation
  
## Progress Made
 * Collision
 * Enemy functionality - attack and death
 * Enemy manager
 * Input buffer and analyzer
 * HUD
 * Player taking damage and death
  
## Sprint/Story Review
I didn't get all that I wanted done this sprint since I didn't take into account how much time implementing collision would take. That throw off my plan for this project and so there are a few mechanics that aren't as fleshed out as I'd like them to be for this submission. I did what I could to get the basic game loop up and going, it will be refined for the next submission.
  
## Dependencies
 I tried my best to have things been relatively clean and only giving them dependencies when needed. Some are obvious (like the enemies depend on the enemy manager) but most of the other are rather deliberate.
### Dependency list 
 * Enemies depend on the creature/player for movement
 * Enemy manager has enemies
 * Collisin handler depends on all the collidable objects in the game
 * Command processor depends on the timed input handler 
 * creature/player depends on the command processor for movement and actions
 * Command processor has a chord analyzer 
 * etc
  
## State
Most of the things/objects in the project are rather stateful. I tend to use states for toggles as well as for noting the status of the moving pieces in the game. For example, both the player and enemies have state to note their status and the game uses and reacts to that status.
  
## Systems
 * Collision system -> handles collision
 * Command Processor and Input -> input buffer and chord analyzer as well as timers 
 * Enemy manager -> manages the enemies
 * Level manager -> intended to load and manage the levels of the game
  
## Patterns Used
 * Strategy -> enemies, weapons
 * Command -> input -> player movement and actions 
 * (pseudo) Object pool -> enemies (pseudo because it's not implementing an Object Pool class like previous demos we've looked at)
 * Singleton -> tag manager 
 * (sort of) Fly Weight -> commands get passed between the chord analyzer and command processor via an enum value rather than actually passing commands through
  
## Not Working
 * Levels system -> level manager doesn't load the levels correctlty
 * Enemies can get stuck on top of each other
  
## Didn't Get to Implement
 * Pick ups and score
 * The overwhelmed player state
 * The stunned enemy state