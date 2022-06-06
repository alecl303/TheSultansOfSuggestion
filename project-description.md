# Basic Game Information

# Summary
Welcome to The Sultans of Suggestion! World peace has been achieved, but humanity’s desire for violence rages on. In this game show of the future, our lucky contestants battle out of the Neo ColosseumTM, as wave after wave of mobs are torn apart to satisfy the bloodlust of contestants and audience alike. If an unlucky participant happens to fall in battle, no matter- there are always more entrants for the most popular program on air. There’s no greater calling- all are welcome to fight and die for the sake of entertainment!

# How to Play
Use WASD or the arrow keys to move around the maps. Press shift to do a quick roll in the direction you're facing (if you're standing still, you can do a dodge in place). You can press or hold left click to fire your ranged weapon and right click to slash with your melee weapon. If a spell is acquired, press Space to activate it. 

# Main Roles

## Producer
Coming into this role with little experience as a project lead, I wanted to try my best to ensure the project's quality by helping the team with aspects they needed assistance with as well as performing the typical managerial duties. In the beginning stages of the project, I scheduled meetings that best fit everybody's time restrictions so that we could discuss the game's design and what our end goals for the game were. After the first few weeks, I gave a heads-up on our Discord server if anyone wanted to get on voice chat and discuss game structure/design/bug fixes. I tried my best to set deadlines, specifically for the alpha that we wanted to present to our playtesters. I also helped to manage our version control and ensure that pull requests were being created and approved appropriately.

Other tasks performed:
* Designed and crafted all the levels, including breaking up the sprite sheets into tiles and determining the game logic needed for the tile grid layers
* In the final days of production, helped to create and manage a [sheet](https://github.com/alecl303/TheSultansOfSuggestion-/blob/b7e197f1e5340d1dfd5356cc596fdb6c6b1a2684/Bugs%20to%20Fix.pdf) full of bugs to fix and quality of life features to implement to improve the game after getting feedback
* General debugging
* Helping in aspects of game feel, what needed to be buffed and nerfed
* Compiled the project description Markdown file from a rough draft Google Doc
* Aided in finding backgrounds for the appropriate scenes along with the main font used
* Created a list of possible buffs/debuffs to use in our card system

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

### Attacking
* Melee attacks spawn an invisible hitbox called a PlayerMeleePrefab, which has a RigidBody2D component, melee hit box controller component, and a Box Collider 2D component. This allows the player melee attacks to have pushback as they follow Unity’s built in physics system. 
* Ranged attacks spawn bullets which also have a RigidBody2D component, bullet controller component, and a Box Collider 2D component. Each bullet will push back the enemy. 
* The player’s attacks are angled based on where the mouse pointer is. The attacks are pointing towards the mouse’s direction.

### General Movement
* The RigidBody2D component’s velocity is used to define the direction and speed the object should be moving toward.
* We avoided using RigidBody2D’s transform.position for movement as it will ignore Unity’s physics system since this is setting the position.

### Determination of Player Movement
* The player's movement is determined using the command pattern and input detection.
* The player's roll direction is a normalized unit vector created using the value of the input axes.
* All directional movement followed exercise 1 from Professor Joshua McCoy.

### Determination of Enemy Movement
* Enemy movement also uses the command pattern (see Game Logic) and the enemy either wanders or chases based on distance from the player.
* To track the player, the enemy simply creates a target vector by subtracting its position from the player’s position, then normalizing that as a unit vector.
* As for the enemy dashing, we utilized Unity’s lerping to make a smooth dash. The dash will activate once the enemy is close enough.

### Collisions
* Between Enemies and the Environment
  * Each environment game object that is collidable on the map has the "Environment" tag. For example, the walls are tagged "Environment".
  * For the Caves map, the empty area and the spikes are labeled "Hole" for the tag. This will allow flying enemies to ignore the collision and go through these parts of the map.  This is done through the EnemyController where, if the enemy is a flying unit and a collision is triggered, the collision is then ignored.
* Between Enemies, Projectiles, and the Player
  * In order to use Unity’s built in physics, we utilized Unity’s RigidBody2D component and Unity’s box/circle collider components. To identify if we want an object to follow Unity’s physics system, the object will have to add those two components. This will allow each object to have some mass and force applied whenever they collide. Once the two box/circle colliders touch, this will inform the attached scripts whether there was a collision. To represent accurate hitboxes, the shape best representing the object is used (except for the player which uses a circle collider to prevent them from dashing through the walls).
*  Projectile Interactions
  * Projectiles are tagged as "PlayerAttack" or "EnemyAttack", and will either collide and destroy themselves or ignore a collision based on the tag. Additional functionality afflicts the projectile's damage on collisions. 
  * Each projectile also has their own defined damage inside a script that is then attached to bullet/projectile prefab. This defines the behavior of the bullet and allows management of stats. 
* Floor Spell Interactions
  * Floor spells are tagged either “PlayerBuffSpell” or “PlayerTrapSpell,” and spawned on the floor. The floor spells will activate their effect in OnTriggerEnter in the EnemyController or PlayerController. PlayerBuffSpell is used for the healing spell, and will activate once the player stands on it. The PlayerTrapSpell is used in IcicleTrap and triggers a barrage of bullets when an enemy steps on the circle. 
  * Each of these has a RigidBody2D and a circle collider component since the spells are both circular. 


## Animation and Visuals
Assets Used:
* Environments
  * [Atlantis Sprite Sheets](https://finalbossblues.itch.io/atlantis-tileset)
  * [Caves Sprite Sheets](https://szadiart.itch.io/rpg-worlds-ca)
  * [Dungeon Sprite Sheets](https://szadiart.itch.io/rogue-fantasy-catacombs)
* Character/Enemies
  * [Main Character, Wolf, Bat, Witch, Golem](https://oco.itch.io/medieval-fantasy-character-pack)
  * [Goblin, Mushroom Man, Flying Eye Guy](https://luizmelo.itch.io/monsters-creatures-fantasy)
  * [Hellhound, Demon Boss](https://ansimuz.itch.io/gothicvania-patreon-collection)
  * [Samurai](https://luizmelo.itch.io/martial-hero)
* Wallpapers/Backgrounds
  * [Mark Ferrari- Sunrise](https://wall.alphacoders.com/big.php?i=705837) - Used in the winning/credits scene
  * [Bytecodeminer - Dark Sun](https://wall.alphacoders.com/big.php?i=911969) - Used in the death scene
  * [Inkpendude- Sword Trials](https://wall.alphacoders.com/big.php?i=1132300) - Used in the main menu
* Other Elements
  * [Various Spell Effects](https://codemanu.itch.io/pixelart-effect-pack)
  * [Sword Sprites](https://thewisehedgehog.itch.io/hs2020)
  * [Spell Sprites](https://assetstore.unity.com/packages/2d/gui/icons/basic-rpg-icons-181301)
  * [Blades for Boss Fight](https://blackdragon1727.itch.io/free-trap-platformer)
  * [Background for Text Boxes and Item Slots](https://assetstore.unity.com/packages/2d/gui/icons/gui-parts-159068)
  * [Health/Mana Bars](https://opengameart.org/content/golden-ui-bigger-than-ever-edition)

Due to a lack of time and artistic expertise, free art assets were used for all the environments, weapons, characters, HUD, and transition screens. Since the narrative follows being on some dark, futuristic game show, there are a variety of environments and enemies to make the player feel as if they are being transported between different biomes. All assets used for the environments were 16x16 or 32x32 (and most other assets were as well). Levels were designed to be progressively more complex and larger to match the increase in difficulty. The exception is the final room containing the boss which is smaller to simplify the fight's design. Every map has walls so the player cannot escape as well as a myriad of ground accessories, both collidable and to make the room more visually pleasing and more fun to play through.

All animations are composed from the included sprite sheets and each character/enemy has their own animation controller. Most of the enemies use some collection of walking and/or running, idling, attacking, and dying animations in their lifespans. The boss's lack of animations from the pack it was obtained from are masked by the fight being based on the bullet-hell aspect. 

## Input
When we were originally designing our game, we wanted to create something simple enough that anyone could play. Due to this, we decided on having four main actions. This works well for our input methods as most controllers have four face buttons or shoulder buttons if the user prefers. Additionally, if the user prefers mouse and keyboard, the mouse has both attacks while the keyboard has the hotkeys for spells and the dodge roll.

### Keyboard and Mouse
The player can move their character by using the ‘WASD’ (or arrow) keys. Pressing Shift will let the character perform a dodge roll to evade enemy attacks. By pressing the spacebar, the character will perform its active spell. By aiming the mouse, players can specifically target and aim at specific high-priority targets.

### Controller 
The player can also play the game with the use of a controller. The player can aim their attacks with the right joystick and move with the left one. Additionally, the spells and dodge roll are bound to their specific face buttons on the controller used.

## Game Logic
Everything mentioned below was either made up or learned from a project done in class.
* Enemy Controller(s)
  * Enemies inherit most functionality from the abstract class ‘EnemyController.cs’, in which they are executing their declared movement and attacks given certain conditions. 
  * This keeps track of stats, handles the coroutines used to buffer states and animations, and handles all functionality related to collisions and status effects.
  * Enemies then extend that class to more specific controllers that declare different IEnemyCommands as their unique attack or movement by overriding the virtual init method, and making a super call on the parent class. Enemy controllers are also used to change their inherent stats.
* Enemy Attack Types
  * IEnemy Command is the interface that determines the way an enemy will behave aside from collisions and stats. These commands will determine what the enemy does in the ‘attack’ state, or the ‘chase’ state.
* Effects
  * Effects are also a unique interface that interact with the player stats. 
  * They are divided amongst stat effects (an abstract class implementing IPlayerEffect) and random effects that also implement the interface. 
  * Each effect contains an execute function that is called when a card is selected along with a name and description that are used to populate cards with text.
  * There are two EffectManager classes used to generate a random buff or debuff. Their sole purpose is to hold instances of each effect and produce a random one when called.
* Spells
  * Spells are another interface much like the IPlayerCommand, but they have a few extra member functions used to provide the player with information such as what the spell does, and how much mana it will cost. They also have cooldown values. 
* Weapons
  * Weapons are a single class that simply generate a random sprite, and a random amount of damage that is determined by rarity.
  * Rarity is determined by a random integer generated between 0-100 and checks for certain thresholds incrementally to determine rarity. The rarity then sets the bounds for the randomized damage.
* Player
  * The PlayerController is a command pattern that executes IPlayerCommands, determines physics collisions, and deals with state transitions and logic.
  * A PlayerAttack is another virtual class that determines basic functionality for any ‘hit box’ that is generated by the player. It has very basic functionality for determining damage and speed.
  * Melee attack is a sprite-less hitbox that simply follows the player after being produced.
  * Ranged attacks (for enemies and player) are bullets that each have a specialized bullet controller to determine flight behavior.
  * The player has a separate call attached to it that is a functionally encapsulated set of stats and stat affecting methods that the PlayerController interacts with.
* Bullet Controllers
  * Bullet controllers interact with the game object’s RigidBody2D component to determine where their direction and speed using vectors.
  * Bullet controllers are each attached to a special bullet prefab that is later referenced for instantiation by an attack.
* Scene Transition / DontDestroyOnLoad
  * Manages scene index by iterating through a set of dialogue canvas objects and then incrementing the scene value.
  * This also sends specific game objects into the DontDestroyOnLoad hierarchy in order to preserve changes from different scenes. 
* Buff/Debuff Cards
  * After all enemies are defeated, the EnemySpawner object which contains the script of the same name, will call EnableStory from DontDestroyOnLoad, enabling the first story box. Once the continue button is clicked, the card selection screen will be enabled from DontDestroyOnLoad. The card selection will randomly generate 3 cards, each one with a different positive aspect (either a passive, weapon, or spell) and debuff displayed in a separate text box. Each card is a button that, when clicked, will apply its card's buff and debuff and any corresponding HUD changes. Inside of each card selection function, a function is called to disable the CardSelection to reset the cycle for the EnemySpawner and enable the next story box. This story box’s continue button will increment to the next scene via the DontDestroyOnLoad.

# Sub-Roles

## Cross-Platform
In addition to standard PC, Mac, and Linux support, we also created a WebGL version of our game and uploaded it to Itch.io so it could be more easily shared with playtesters and played in browser without needing to download anything beforehand. Our game could work on mobile devices with additional time, however we did not have enough time to create an enjoyable experience so we decided not to support mobile platforms.

## Audio

### Crafting the Tunes
* [Super Audio Cart](http://www.superaudiocart.com/) was used for all samples. They are all sampled from retro consoles. Ours specifically use NES, SNES/Famicom, Sega Genesis, and Atari 2600 samples. 
* Midi Controller: [MAX49 Keyboard MIDI Controller with CV Output | Akai Pro](https://www.akaipro.com/max49)
* DAW : [FL Studio](https://www.image-line.com/fl-studio/)

### Sound Manager
The sound manager holds all of the game’s sound assets and persists between levels by being added as a DoNotDestroy object. On load, the level’s canvas element will pick the track using the location trigger script similar to that of Project 1.

### Inspiration Behind the Themes

#### Menu 
This theme was meant to be a semi ‘poppy’ loop with an upbeat drum loop. I used a Famicom VRC7 wave synth sample on top of major triad arpeggios, coupled with a minor fourth to create the main synth loop. The drums were an SNES hip-hop themed drum kit set. 

#### Atlantis
This theme was my take on the ‘chill underwater groove’ trope seen in many other retro video games. I made use of major 7th/13th chords by inverting the 13th down an octave. I then used a dissonant half step progression of the same chord voicing to create the somewhat melancholy tone. After looping through a 1-7-5 progression a few times, I made use of pitch blend to make the song sound like it was eerily warping.

#### First Caves Room
Drum and bass all the way for this one. I made use of a mixture of major triads, minor triads, and one augmented fifth chord to make a very slightly dissonant progression. The drums were a classic syncopation groove over the 4/4 time signature and the lead was a basic square lead to get a crunchy sound.

#### Dungeon
This song was loosely inspired by Kefka’s Theme from Final Fantasy 6/3 and it is surprisingly almost all in the C major key, with the exception of the dissonant lead just after the intro. The drums here were actually all made using the Atari 2600 sound kit samples, which were used originally for things like attacks, enemy sounds, and menu sounds. The dungeon gave me a very ‘Castlevania/Final Fantasy boss fight’ vibe, so that’s where I got most of these progressions.

#### Second Caves Room
This song was originally supposed to sound like a Shin Megami Tensei: Nocturne song, but after soloing over it a bit with my Midi controller, it went completely off the rails and became more of a funk song from Sonic Mania. 7th chord triplets for chords and pentatonics for the lead, bass, and drums.

#### Boss
This song was a weird fusion of technical death metal and [Counter Hunter 2 theme from the Mega Man X series](https://www.youtube.com/watch?v=d6KcZBTcPCk). The original version was pretty dry though, so I added an ‘Undertale-esque’ triumphant lead to end the loop off with. Much more drum heavy, with use of blast beats, break downs, and technical death metal style bass fills/arpeggios.

#### Game Over
This song was 100% inspired by the iconic Super Mario World game over jingle. It was originally just a single loop jingle like the example, but I had some fun turning it into an 8 bit lofi-ish track, and the rest of the group seemed to like it better. Thus, it became a longer theme with some sound sample percussion. 

## Gameplay Testing
Gameplay testing was conducted with a rough alpha version of our game. The alpha had no storyline and was missing a few key features such as a boss health bar and damage numbers. We prioritized feedback around the game feel and how enjoyable the game was to play on that current version. All answers were compiled into a spreadsheet that can be seen [here](https://github.com/alecl303/TheSultansOfSuggestion-/blob/0d9b0e6d5da76aebd19769a5b863e955ef6e585f/Playtester%20Comments-%20Sultans%20of%20Suggestion%20-%20ECS%20189L%20-%20Sheet1.pdf). We found that many people wanted enemy health bars, along with the unimplemented damage numbers, just so they could see how much damage they were truly doing. It was also discovered that the melee weapon was just too strong and the ranged too weak so those were balanced along with the enemies slightly. Story/flavor text was also added as suggested. 

## Narrative Design
Since we were working on creating a roguelike, we wanted our story to be mainly in the background and not too intrusive, focusing more on the setting rather than the plot (this isn't to say that roguelites/roguelikes can't have a story - see [Hades](https://store.steampowered.com/app/1145360/Hades/)). Set in a hypothetical utopia free from danger, one game show glorifies violence to satisfy humanity’s needs. Contestants are pitted against waves of enemies to fight their way out of the Colosseum. The player is placed in many different biomes with varied room structures to give the audience a better experience. The narrative is mainly present in flavor text during transitions at the beginning and end of each room. The host of the show comments on the room and player’s choices, while also giving some tips to the player. Rather than an underdog story, the show aims to make the contestant as heroic as possible- the focus is on the slaughter of enemies, so it has to be made to look and sound effortless.

  
## Press Kit and Trailer
  
### Press Kit
[Press Kit](https://github.com/alecl303/TheSultansOfSuggestion-/blob/0d9b0e6d5da76aebd19769a5b863e955ef6e585f/Presskit.md) - The press kit is available to see here. The press kit contains a list of information including release date, supported platforms, price, languages, ESRB, and genre. There is also a short description and gameplay explanation, coupled with a few images to show the game. I chose to include these to give a brief overview of what the game is, while also demoing a part of the game using a trailer and images. The goal is to give the player a taste of what the game is about and to attract them without giving all the mystery away. 
  
[Trailer](google.com) - The trailer is made with the same intentions as the press kit by giving the player a snapshot of the game. This will be in the form of actual gameplay, in the form of enemies, spells, cards, and the boss. EDIT THIS

## Game Feel
After receiving feedback from gameplay testing and just playing around with the game, I met with the team to discuss what changes we wanted to make to the game to improve the feel.
  
### Balancing
  * After being informed that melee weapons were quite overpowered, we decided to significantly lower the melee damage. Melee attacks would previously 2-3 shot stronger enemies, decreasing their ferocity. While legendary weapons still provide a lot of damage, they have a low spawn chance in the cards.
  * To go along with the melee changes, the ranged weapon was too weak so its damage numbers were buffed.
  * Bullets from the player's ranged weapon now have zero mass to lower the knock back caused by the bullets.
  * Some card debuffs were either too powerful or just unfun to play with. The debuff to lower the player to 1HP was removed along with the ability to lose the player's inherent roll ability which significantly lowered movement.
  * Due to relatively low damage from the enemies due to their high attack rate, player health and the healing between scenes were lowered due to the ability to tank most hits without consequence, especially during the boss fight. 
  * The Heal spell duration was decreased since mana regeneration was high. 
  * The final boss' damage and health were both increased to make the fight more challenging. 
  * The small chance for elite mobs with increased health and damage were added to also increase difficulty. 
  * The freezing spell AOE was increased to improve its damage output.
  * All poison buffs now give an increased chance of poison.

### UI Changes
  * Arial was being used as base font across most areas that contained text. This was changed to a cooler font.
  * The rainbow effect on the title was removed due to common dislike of its appearance (and it didn't match the dark theme of our game). 
  * Added sprites for all menu items- spells and weapons.
  * Added health bar and damage numbers for enemies that also show different damage types, poison, and critical strikes (these were not present in the alpha).
  
### User Gameplay
  * Diagonal movement was nerfed since it was faster than plain lateral or vertical movement. 
  * Melee attack nerfed such that the player cannot spam hits insanely fast. 
