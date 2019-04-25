
# Game Setup

Definitions for how things are organized for this specific game.

- [Idiom Preferences](#idiom-preferences)
- [Layers](#Layers)
- [Scene Buckets](#scene-buckets)
- [Tags](#Layers)
- [Z-Depths](#z-depths)




# Idiom Preferences

General preference when choosing between competing idioms. Non-exhaustive list of what will typically be employed in the project, exceptions are fine:

- *Coroutines* over State Machines
- *Nested Prefabs* over Linked Prefabs
- *Self Event Management* over Selecting Objects With Input
- *Inheritence* over sibling monos
- *2DPhysics* over translation
- *Events* over messaging
- *Singletons* over global variables
- Group objects by *tags* over layers, name, type, links
- Locate objects by *layers* over tags, name, type, links
- Persist data through *PlayerPrefs* over persistent objects
- *Find* objects over self-registration


# Layers


##### Enemy
Enemy units that have colliders to interact with the environment

___

##### EnemyTrigger
Devices used in the scene to detect trigger-collisions with the *Enemy* layer only

___

##### Player
Player units that have colliders to interact with the environment

___

##### PlayerTrigger

Devices used in the scene to detect collisions with the *Player* layer only



# Scene Buckets
Organize Game Objects into standard buckets that are at `0,0,0`.

##### _Cameras
##### _Controllers
##### _Managers
##### _Stage
- Background
- OnStage
  - Actors
  - Props
  - Devices
- Foreground
##### _UI 



# Tags
___
##### EnemyUnit
All units controlled by the Enemy that should be on a general search

*Use*: See if any Enemy Units exist

___

##### UICanvas
All screen overlays that should be found on a general search

*Use*: Hide all canvases and display one

# Z Depths
Basic organization of Z-Depth:
- Cameras at `-2`
- Stage at `5`:
  - Background at `5` (0 to parent)
  - OnStage at `3` (-2 to parent)
  - Foreground at `1` (-4 to parent)
