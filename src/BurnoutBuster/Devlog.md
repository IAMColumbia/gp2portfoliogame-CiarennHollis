# Devlog
  
## 27 February 2024 | 17:56
### Check In
 * Finished implementing the command pattern bits. Some of it will probably have to be refactored once I add in the player bits.
 * I am currently very much leaning towards having the movement stuff not be ran though the command system, just the actions (attack, heavy attack, etc.) this might make things weird with implementing the dash mechanic but we will see.
  
## Next Steps
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