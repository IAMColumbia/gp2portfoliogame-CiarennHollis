# Postmortem

## Challenges
The big challenges that I hit with this project were the combat combo system and the CommandProcessor that handles that, implementing collision and hit boxes, and implementing animations. The devlog for this project details how I went about solving these things.

## Reusability
The core pieces of the codebase aren't MonoGame dependent. The creature, enemy (and various types of them), swords, etc. isn't dependent on MonoGame. Likewise, the logic for the command processor handling the combat combos isn't really dependent on MonoGame (though it may look like it is because it's dependent on a game loop); the core bits of it could be reused with a different platform with a little bit of refactored to account for that platform's game loop. The collision system is kind of MonoGame dependent since it relies on the way MonoGame defines and handles rectangles; but with some refactoring it could be used with another platform, though the need for this isn't that high since the majority of game engine have their own solutions for collision. 
How all the pieces of the game interact with each other, however, is dependent on MonoGame. If this game were to be recreated on a different platform, how all these parts interact with each other would need to be redesigned.
    Rate how reusable the game codebase is? What percentage is specific to the game? What percentage is specific to the platform (monogame/unity)?

## Maintainability
Most of the parts are relatively maintainable. It's relatively easy to add more enemies and to add more weapons. Because I underthought the ItemManager, having it manage more than the weapons isn't easy. It needs to be refactored and built out some more to be maintainable.
  
## Completion
The game is mostly done, the majority of the things that still needed to be done were aesthetic things.
  
## Technical Debt
 * Player seems to only hit one enemy at a time even if there is more than one in the hitbox.
 * There are magic numbers in some places (there's comments in the code calling this out)
 * The way the game determines if the player has won is a bit of a Hack (called out with comments in the code
 * The swords are constantly checking whether or not their animations list exists since it keeps getting set to null at some point (discussion about this issue can be found in the Devlog.md file), so if it finds it is null it sets up all the animations again before playing the desired animation
 * Hard coded win 
 * Game pad implementation doesn't work
 * Responsibility/Separation of concern issues in the CommandProcessor (noted with a comment)
 * Level manager isn't implemented; it was abandoned
 * Dirty delete when the player discards the old sword.
  
