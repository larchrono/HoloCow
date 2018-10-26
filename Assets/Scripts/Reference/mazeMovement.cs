using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class mazeMovement : NetworkBehaviour   
{
	private Quaternion rot;
	private bool gyroEnabled;
	private Gyroscope gyro;

	float a;
	// Use this for initialization
	void Start () 
	{
		
//		Input.gyro.enabled = true;
//		transform.rotation = Quaternion.Euler(90f,90f,0f);
//		rot = new Quaternion(0, 0, 1, 0);
//
//		transform.rotation = Quaternion.identity;
		gyroEnabled = EnableGyro();
	}

	private bool EnableGyro()
	{

		if (SystemInfo.supportsGyroscope)
		{

			gyro = Input.gyro;
			gyro.enabled = true;

			transform.rotation = Quaternion.Euler(90f,90f,0f);
			rot = new Quaternion(0, 0, 1, 0);

			return true;
		}
		return false;
	}


	void Update() // runs 60 fps or so
	{
//		if (!isLocalPlayer)
//		{
//			return;
//		}


////		transform.rotation = Input.gyro.attitude;
//		transform.Rotate (-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.z, -Input.gyro.rotationRateUnbiased.y);
//		a = -Input.gyro.rotationRateUnbiased.x;
//		Debug.Log (a);


		if (!isLocalPlayer)
		{
			return;
		}
		if(gyroEnabled)
		{
			Debug.Log ("Start rotate");
			transform.rotation = gyro.attitude * rot;

		}


	}

}