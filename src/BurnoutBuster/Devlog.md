# Devlog
  
## 30 March 2024 | 9:16
### Check In 
 * Need to make some headway on this project today
	* I was still unable to get the collision thing from Monogame Extended to work :P I'm not really sure what the issue is. 
	* So, I just might write my own collision component :P
  
### Goals
 * Finish enemy manager
 * Collision
 * analyzer and buffer :P
  
## 29 March 2024 | 13:25
### Check In
 * Started buidling out the enemy manager -> I made it a drawable game componenet so that it could call draw on the enemies. It will be the thing initializing, updating, and drawing the enemies. It will also have a pool of enemies to pull and "spawn" enemies from*
	* I need to test this out to see if to see if it works
  
### Next Steps
 * Finish building out the enemy manager
 * Implement levels and rooms
 * Implement HUD
 * Implement buffer and analyzer for input combos
 * Look into Monogame Extended collision and maybe ask Jeff for help with it :P
  
## 29 March 2024 | 12:41
### Check In
 * So, I had implemented the collision system from the Monogame Extended library, but I can't get the system to work for some reason. I'm unsure what I'm doing wrong with it
	* So, since I've spent the last hour trying to get that system to respond to me, I'm going to work on something else for now :P
  
### Goals
 * Enemy manager
  
## 29 March 2024 | 11:31
### Check In
 * Sitting down to work on this project
  
### Goals
 * Get the enemy class built out
 * Build out the enemy manager 
 * Decide how levels/rooms will work in this game
 * Implement a HUD
 * Implement an analzer for the input combos
  
  
## 20 March 2024 | 14:02
### Check In
 * So, I hit this question of how do I handle collision for this game. I wanted to have a system that was relatively similar to how Unity implements it's collisions so that I could have a method similar to Unity's OnCollisionEnter().
 * I don't really have the time to sit down and write a custom physics/collision system for this project, while that probably would be neat to do (and I may do so at some point in the future just to do it). Because I don't have that time, I looked around at other ways to bring in a collision system into this project.
 * I eventually found the MonoGame.Extended.Collision library and was able to install it into the project. 
 * To go along with the collision system, I also wrote a simple tag manager so that I could give tags to things and have check to if things are tagged rather than checking their type. Instead of using strings for tags though, I am using an enum.
 * These two things together in theory should let me implement this collision sysem in a way that's relatively similar to doing so in Unity. I just wrapped the ICollisionActor from MonoGame Extended and my ITaggable into it's own interface ITaggedCollidable so it would look a little cleaner and be simpler to implement.
 * I kind of forgot to check in with this doc before implementing the things listed above since I didn't intend to do work on this project when I sat down. I kind of just ended up working on it a bit since I got bored :P


## 18 March 2024 | 16:29
### Check In
 * Didn't get as much done today as I wanted to :P
 * Really only get some simple enemmy movement in
  
### Next Steps
 * Continue implementing the enemy bits
 * Implement the enemy manager
  
## 18 March 2024 | 11:53
### Check In
 * I've gone in and made new issues for the tasks for this sprint
 * Today I'm hoping to build out the functionality of the enemies
  
### Goals
 * Enemy movement
 * Enemy collision 
 * Enemy attack
 * Enemy state
 * Start Enemy manager
  
  
## 11 March 2024 | 16:47
### Check In
 * I added in a janky little collision thing so that the combat system can be demoed in a more substantial manner. Collision for the enemies probably should be handled through an enemy manager but that was outside the scope of the POC milestone for me.
 * Added instructions to the screen so demoing the POC would be easier
 * Cleaned up unused using statements

  
### Next Steps 
 * Ask Jeff about the animatable sprite 
 * Enemy movement
 * Enemy manager: spawning enemies, collision for the enemies
 * I'm sure there's other things that I can't think of right now and I'm sure there'll be things that come up in class tomorrow. I can also look at my proposal doc since I noted down things there. Those tasks will be housed in GitHub Issues though.
  
  
## 11 March 2024 | 14:16
### Check In
 * So, the combat system and combat combos is successfully implemented!! [ISSUE #5 CLOSED]
 * The original design I drew up for them also had "cool down" timers on them. I'm not exactly sure how I want to implement this gameplay-wise. I have those there because I don't want players to be able to spam the heavy attack button and just deal out heavy attacks. But there could be a better way of implementing this limitation with. I want to look at how other games (like Hades) handle this in their gameplay. Because of this this particular aspect will be something for the next milestone (the vertical slice).
 * The working combat-combo-system I think meets the POC requirement. I would like to make it possible for the play to hit the enemy that is currently in the scene. But I haven't decided how I want the enemy behavior to be structured. I have written the enemy manager yet.
 * Added sprite for the enemy [ISSUE #6 CLOSED]
  
### Next Steps
 * Think about how implement the enemies taking damage and the manager.
 * Write documentation for the POC
  
## 11 March 2024 | 12:02
### Check In
 * Started implementing using listening states in the command processor.
 * I found that the timed input bits I had in the TimeInputManager I probably won't use (the PressedKey var) since I realized that those were only really being used for seeing if a key was double pressed. I might still use that for the Dash Attack since that combo was meant to be: Dash + attack + attack. But I think, to make my life a little easier, I might just have the dash attack be: Dash + Attack.	
 * Started implementing the combos. I abstracted out how the processor will respond based on the listening state to their own methods and have those methods return commands so things fit nicely back into the way the command processor functions.
 * Taking a break for lunch :P
  
### Next Steps
 * Continue implementing the combat combos
 * Start with continuing to build out the methods in the command processor for the listening modes [DONE]
  
## 11 March 2024 | 10:37
### Check In
 * Slept on it. Will continue where I left off yesterday
  
### Goals
 * Implement the timed input bits in the command processor [DONE]
 * Implement listening states for the combat combos [DONE]
 * Implement combat combos [DONE]
 * Make place holder sprite for the enemy [DONE]
  
  
## 10 March 2024 | 16:20
### Check In
 * Created the command class for all the actions and set things up for those actions to be implemented in game
 * [ISSUE #4 CLOSED]
 * I've looked at this too long today and need to take a break. Will continue working on this tomorrow.
  
### Implementing the timed input stuff
 * So, the EmojiJoy project I originally wrote the TimedInputHandler class for handled movement combos by having different states and listening for different thing based off of those states. It's states would change based on the times that were attached to the PressedKey that would be instantiated whenever arrow key input was detected.
 * In EmojiJoy, the EmojiController handled those listing states but for this project, the CommandProcessor will do that. Instead of what input it's listening for being hard coded, it gets read in from a KeyMap and there is a switch the CommandProcessor uses to handle that. 
 * I'm thinking that I could have methods for the action buttons and those methods will create and execute the new command rather than that switch itself so that I can implement the listening states and have the processor create and execute the commands based on what particular action it's listening for. 
 * Having the creation of the action commands be separated out into different methods (rather than in the switch) would in theory also let me use the timers on the pressedKey vars that would be created and stop those timers if the game sees that a player wouldn't be using a combo and can thus adjust its state accordingly.
  
### Next Steps 
 * Implement the timed input bits in the command processor
 * Implement listening states for the combat combos
 * Implement combat combos
  
  
## 10 March 2024 | 15:55
### Check In
 * Implemented the weapon bits. I also used the strategy pattern for this for the same reason as the enemies.
 * [ISSUE #7 CLOSED]
  
### Next Steps
 * Implement the combat/action commmands
  
## 10 March 2024 | 15:19
### Check In
 * Implemented the enemy bits. I used the strategy pattern for it so that it would be easier to have several different variations of enemies.
 * But for now, there is only one type of enemy since the focus for this milestone is getting the combat system up and running.
  
### Next Steps
 * Built out the weapons system a bit so that the combat system can be tested. My intention is to have the player's attack be dependent on the weapon they have.
  
## 10 March 2024 | 14:21
### Check In
 * Going to implement the combat system bits: Setting the commands for those actions and getting them to work with the player and command processor system
 * I also want to try to finish up the rest of the tasks noted in Github Issues for the POC milestone
  
### Goals
 * Implement combat commands
 * Implement simple weapon system that can be built on [DONE]
 * Implement simple enemy to be built out further later [DONE]
  

## 2 March 2024 | 16:05
### Check In
 * Basic player movement is implemented. 
	* I tried to have the MonogameCreature inherit from the DrawableAnimatableSprite, but I'm not entirely sure how this class works so I wanna ask Jeff about it. It would be nice to have that so I can implement animations. But the animations aren't imperative at this time so I just used the DrawableSprite.
 * I did end up having the movement and combat system both be ran through the command processor. I had thought about having them just be separate since I was going to us Jeff's PlayerController. I didn't know hard it would be to implement the command pattern for the movement and actions together, but once I had implemented the movement, the actions/attacks/combat move commands were basically the same set up. Plus, it does make sense the two would be together since they both are dependent on input.
 * Set up the structure for the combat, the command types need to be made and the command methods on the CommandCreature need to be built out.

### Next Steps
 * Ask Jeff about the DrawableAnimatableSprite
 * Implement the combat actions
 * Think about and implement the combat combos
  

## 2 March 2024 | 13:31
### Check In
 * Going to start implementing the player bits: player class, movement, and set up the structure for the combat
  
### Goals
 * Write the player script [DONE]
 * Implement player movement [DONE]
 * Implement initial structure for combat (doesn't have to work yet) [DONE]

  
## 27 February 2024 | 17:56
### Check In
 * Finished implementing the command pattern bits. Some of it will probably have to be refactored once I add in the player bits.
 * I am currently very much leaning towards having the movement stuff not be ran though the command system, just the actions (attack, heavy attack, etc.) this might make things weird with implementing the dash mechanic but we will see.
  
### Next Steps
 * Implement the player bits 
  
## 27 February 2024 | 16:25
### Check In
 * Going to finish up some stuff from yesterday, mainly implementing the command pattern stuff.
  
## Goals
 * Implement the command pattern stuff based on jeff's examples.
  

## 26 February 2024 | 17:13
### Check In
 * I didn't finish everything I wanted to today, but I did make some good progress.
 * Created this file. [ISSUE #8 CLOSED]
 * Brought in the old script from the Emoji Joy project. [ISSUE #2 CLOSED]
 * I saw that Jeff's InputHandler uses an enum to store the gamepad buttons, but it's only local to it's particular class. I made an enum to compliment that that is accessible to the wider project. I made sure the values of my new enum matched that of Jeff's so I can cast variables between them without an issue. [ISSUE #3 CLOSED]
 * I made classes for the pressed key and pressed button, I was unsure of what class those two would inherit from since they are essentially the same, just one is for a key press and the other is for a button press. [ISSUE #3 CLOSED]
 * I also expanded my TimedInputHandler.cs so it can also handle a gamepad since I would like this project to support play with a gamepad. [ISSUE #3 CLOSED]
 * I noticed that Jeff's MonogameLibrary has a PlayerController.cs script. I might just use that for the player's movement and not have that movement input ran through the command processor (that processor would then just handle input for the ingame actions/attacks) since it already handles movement with a game pad. I haven't decided yet and I want to look at that class more before I decide. 
 * I added the initial scripts for implementing the command pattern but haven't built them out yet. [ISSUE #5]
 * Brought in the place holder sprite for the player.
  
### Where I Left Off
 * I had just added the scripts and folders for implementing the command pattern to the project
  
### Next Steps
 * Continue implementing the command pattern
 * Start building out the player class
  
## 26 February 2024 | 13:36
### Check In
 * The project files have been created and currently run the way they should (displaying plain old corn flower blue).
 * Had some issues with Windows not allowing the project to build the dotnet-tools.json files, but that has been fixed.
  
### Goals
 * Bring in old scripts from the Emoji joy project [DONE]
 * Implement the timed input system from the Emoji Joy project [DONE]
 * Build out the player class
 * Build out structure for command pattern [STARTED]