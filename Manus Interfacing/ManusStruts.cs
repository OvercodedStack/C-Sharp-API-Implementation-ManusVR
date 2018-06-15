///////////////////////////////////////////////////////////////////////////////
//
//  Original System: ManusHandConverter.h/cpp
//  Subsystem:       Human-Robot Interaction
//  Workfile:        ManusHandConverter.cs
//  Revision:        1.1 - 6/11/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Data structure design for the Manus hand devices. Designed to be readily called as a API wrapper to the Manus.h SDK. Refactored for C#. 
//
///////////////////////////////////////////////////////////////////////////////
using UnityEngine;
namespace manus_interface {
	public struct quarternion {
		public double x, y, z, w;
	};

	public struct vector_manus {
		public double x, y, z;
	};

	public struct pose {
		public Vector3  translation;
		public Quaternion  rotation;
	};
}