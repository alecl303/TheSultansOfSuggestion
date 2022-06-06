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

### Game Scene

####  Level Entry
This canvas is enabled in every scene/level. This scene freezes all enemies as well as the player while also displaying some story text for the user. The game begins when the player hits the Continue button.

#### Normal Gameplay
During normal gameplay/combat, the UI has several elements. To start, there is a simple HUD with a health bar and mana bar that depletes and refills depending on the user's actions. These bars are built on sliders that are connected to the player. There is also a 3 slot item bar which houses the range attack indicator, weapon, and spell. This bar can change when a new weapon or spell is picked. Enemy health bars are also built from sliders which are connected to each respective enemy and indicate their current health ([referenced this tutorial](https://www.youtube.com/watch?v=ZYeXmze5gxg)). The bars change whenever the enemy takes damage and fully disappear when they die. Damage popup numbers appear whenever you attack an enemy ([referenced this tutorial](https://www.youtube.com/watch?app=desktop&v=iD1_JczQcFY)). The numbers grow in scale and fade over time. Additionally, normal attacks will generate white numbers, crits red numbers, and poison green numbers. Crit damage popups are scaled larger than normal ones to make it more noticeable and satisfying.

#### Post Room Completion

### Pause Menu

### Death Scene


## Movement/Physics

## Animation and Visuals

## Input

## Game Logic

# Sub-Roles

## Cross-Platform

## Audio

## Gameplay Testing

## Narrative Design

## Press Kit and Trailer

## Game Feel
