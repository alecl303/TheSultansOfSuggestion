# Basic Game Information

# Summary

# How to Play

# Main Roles

## Producer

## User Interface

### UI Overview
For the user interface we have several components. Starting off, we begin in a main menu screen that allows the user several options. Within the actual game, there is a pause menu, a simple HUD, enemy health bars, a boss health bar, story text, and a card selection screen. These UI elements were implemented with a mix of different scenes, canvas objects, and Unity’s TextMesh Pro elements.

### Menu Scene
In this scene we implemented a canvas with four separate buttons. Play begins the game by loading the next scene. Options enable a separate canvas and disables the current canvas. This options canvas has a slider that currently controls the volume and has a back button which disables the option canvas and reenables the main menu canvas. The Controls button goes through the same transition phase as options, but opens another canvas which displays controls for keyboard and mouse. This menu also contains some animated sprites to demonstrate the actions for the controls. Lastly, we have the quit button which should close the .exe file if you are playing on the built version of our game.

### Game Scenes

####  Level Entry
This canvas is enabled in every scene/level. This scene freezes all enemies as well as the player while also displaying some story text for the user. The game begins when the player hits the Continue button.

#### Normal Gameplay
During normal gameplay/combat, the UI has several elements. To start, there is a simple HUD with a health bar and mana bar that depletes and refills depending on the user's actions. These bars are built on sliders that are connected to the player. There is also a 3 slot item bar which houses the range attack indicator, weapon, and spell. This bar can change when a new weapon or spell is picked. Enemy health bars are also built from sliders which are connected to each respective enemy and indicate their current health ([referenced this tutorial](https://www.youtube.com/watch?v=ZYeXmze5gxg)). The boss has a larger health bar located at the top of the screen due to it being more significant. The bars change whenever the enemy takes damage and fully disappear when they die. Damage popup numbers appear whenever you attack an enemy ([referenced this tutorial](https://www.youtube.com/watch?app=desktop&v=iD1_JczQcFY)). The numbers grow in scale and fade over time. Additionally, normal attacks will generate white numbers, crits red numbers, and poison green numbers. Crit damage popups are scaled larger than normal ones to make it more noticeable and satisfying.

#### Post Room Completion
This canvas appears after all enemies are defeated in the current stage. Upon killing all the enemies, this should trigger a canvas to load displaying some more story text. After dismissing that text, a card selection screen is enabled. Here there are three random cards, each card has a randomly generated buff and debuff. Card one displays a stat buff, card two displays a new random weapon, and card three has a new spell. There is a description for each buff and debuff generated. Choosing either a new weapon or spell, also changes the HUD item bar. After choosing another text box will appear with some flavor text depending on what was chosen. After that box is closed, the next level is loaded. When either one of the dialogue text boxes is open, the player is free to move around as they wish but they cannot continue until they press the respective button.

### Pause Menu
If the player is participating in normal gameplay and has not reached during the room completion phase, the player can hit Esc in which a pause canvas will be loaded and the game will be paused. In this canvas, there is a resume button that continues the game, an options button for the volume again, a controls screen, and a quit button that leads back to the main menu screen.

### Death and Credits Scenes
If the player's health depletes to 0, then a death scene loads which informs the player they died and allows for a direct continuation which resets the player back to the first level or a main menu option that returns them to the main menu. The credits scene is very similar to death but it portrays a more cheery connotation then its counterpart.

## Movement/Physics

## Animation and Visuals

## Input

## Game Logic

# Sub-Roles

## Cross-Platform

## Audio

## Gameplay Testing

Gameplay testing was conducted with a rough alpha version of our game. The alpha had no storyline and was missing a few key features such as a boss health bar and damage numbers. We prioritized feedback around the game feel and how enjoyable the game was to play on that current version. All answers were compiled into a spreadsheet that can be seen <here>. We found that many people wanted enemy health bars, along with the unimplemented damage numbers, just so they could see how much damage they were truly doing. It was also discovered that the melee weapon was just too strong and the ranged too weak so those were balanced along with the enemies slightly. Story/flavor text was also added as suggested. 

## Narrative Design
Since we were working on creating a roguelike, we wanted our story to be mainly in the background and not too intrusive, focusing more on the setting rather than the plot (this isn't to say that roguelites/roguelikes can't have a story - see [Hades](https://store.steampowered.com/app/1145360/Hades/)). Set in a hypothetical utopia free from danger, one game show glorifies violence to satisfy humanity’s needs. Contestants are pitted against waves of enemies to fight their way out of the Colosseum. The player is placed in many different biomes with varied room structures to give the audience a better experience. The narrative is mainly present in flavor text during transitions at the beginning and end of each room. The host of the show comments on the room and player’s choices, while also giving some tips to the player. Rather than an underdog story, the show aims to make the contestant as heroic as possible- the focus is on the slaughter of enemies, so it has to be made to look and sound effortless.

  
## Press Kit and Trailer
  
### Press Kit
[Press Kit](https://github.com/alecl303/TheSultansOfSuggestion-/blob/fix-dialogue-boxes/Presskit.md) - The press kit is available to see here. The press kit contains a list of information including release date, supported platforms, price, languages, ESRB, and genre. There is also a short description and gameplay explanation, coupled with a few images to show the game. I chose to include these to give a brief overview of what the game is, while also demoing a part of the game using a trailer and images. The goal is to give the player a taste of what the game is about and to attract them without giving all the mystery away. 
  
[Trailer](google.com) - The trailer is made with the same intentions as the press kit by giving the player a snapshot of the game. This will be in the form of actual gameplay, in the form of enemies, spells, cards, and the boss. EDIT THIS

## Game Feel
