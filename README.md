## General game idea
This is a small management game for a school project. 
The general goal of the game is to reach zero emissions by some year. The user needs to invest money on different upgrades that influence a continent's yearly income and emissions.

## What does this repo contain?
This repository contains the Unity project files for the game. This includes things like scripts, 3D-models, music etc. One can clone the repository and open the project with unity. Unity Editor version `2021.3.11f1` was used for this project.

## Credits
* Thanks to [Konstantin_Keller](https://sketchfab.com/Konstantin_Keller) for making the earth 3D-model. The model is licensed under [CC Attribution](https://creativecommons.org/licenses/by/4.0/) and can be found [here](https://sketchfab.com/3d-models/low-poly-earth-c99483d5e2a94ca8b4f3579145584beb#download)

### Quick notes about things that can be improved, refactored and or optimized in the future:
* Upgrade system
* Use sub-class for upgrades with special effect
* Audiomanager
* UI Coding (opening menues etc.)
* Object pooling for creating upgrade ui elements (see fps drop in profiler when updating refreshing upgrade menu)
* Globe 3D-model
* Zoom and spinning of globe
* Prober savesystem
* Scriptable objects for saving catastrophe data

There's also comments in the codebase, look for TODO, !FIXME etc. keywords.

#### NOTE: This is only a prototype and the gameplay needs a lot of work.
