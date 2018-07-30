# C# API Implementation ManusVR
Author: **Esteban Segarra**
Primary mantainer: **Esteban Segarra**

# Introduction

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

## Example Usage

While not the best way of learning how to use this API, this unity project extensively used this API and contains a few scripts that can be used to move data around if need be. The best way to learn it will be to navigate to  Assets>scenes >Box_wall_test.unity https://github.com/OvercodedStack/Manus_VR_UR5_Repo 

The prefabs folder also contains some prefab objects containing hands with Manus VR API integrated (although it might be an old version at this point).

The gameobjects containing the hands have most of the code integrated under a child gameobject as left/right-hand. You can also see any additional code that goes in the background if you're interested in manipulations. 

Please note that while the project was developed with the VIVE/OpenVR system, it is NOT a requirement if you have different intentions. 

## Acknowledgements
- Megan Zimmerman for providing the initial code
- Manus VR
- The NIST SURF program
- Licensed Under Apache 3.0 
- Albert Hwang for test Scene
- SteamVR 
