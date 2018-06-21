# C# API Implementation ManusVR
Author: **Esteban Segarra**
Primary mantainer: **Esteban Segarra**

This is a parallel project running alongside the C++ implementation of the Manus VR hand set, which can be found [here](https://github.com/OvercodedStack/C-Plus-Plus-API-Implementation-Manus-VR). This API aims to be as efficient as possible while combining the data collected from the Manus VR devices and stringing them together into a data structure that's more sensible than what the Manus VR SDK provides at present (Manus VR SDK in use: v1.1.1 with the 64-bit implementation). The API can work with one or two hands simultaneously connected to the computer.

This project was heavily developed for use under Unity 3D to run as a stand-alone scripts that run on the Unity scene. This project alson includes some utility scripts which could be used to better understand the ManusVR data. 

## Requirements:
Manus VR SDK V1.1.1
Unity 3D 2017 2.1 or better. 

## Quick Start
Load the solution file for the program. Compile the program with your required needs and then run as needed.

Note: The ManusVR SDK can be located in a Unity project within a plugins folder.  Ex:

/Plugins

## Use
This API was OOP designed with the use of the header-only SDK provided by Manus. The program is split into 4 header and implementation files as stated below:

- Finger.cs *A finger class for each manus hand. Contains data relating to raw, quaternion/vector data, and Manus Profile data.*

- Manus_hand_obj.cpp/h *The representation of the Manus VR hand itself. Contains arrays of finger data, positional, and wrist data*

- Manus_interpreter.cs *Concerns itself with reading in the data collected from the SDK and turning it into data structure using one of the previous data formats.*

- ManusHandConverter.cs *A breif example program that utilizes the data collected. The update loop contains some public variables and options that can be utilized to debug the ManusVR device.*

In addition, there is a previous implementation by Megan Zimmerman that utilized the SDK previously.

## Acknowledgements
- Megan Zimmerman for providing the initial code
- Manus VR
- The NIST SURF program
- Licensed Under Apache 3.0 
- Albert Hwang for test Scene
- SteamVR 
