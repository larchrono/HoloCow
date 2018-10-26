using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroBack : MonoBehaviour {

	private bool gyroEnabled;
	private Gyroscope gyro;

	public GameObject cube;
	private Quaternion rot;

	private void Start(){
		//cameraContainer = new GameObject ("camera Container");
		cube.transform.position = transform.position;
		//transform.SetParent = (cube.transform);
		gyroEnabled = EnableGyro ();
	}

	private bool EnableGyro(){
		//Debug.Log (SystemInfo.supportsGyroscope);
		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;

			cube.transform.rotation = Quaternion.Euler (90f, 90f, 0f);
			rot = new Quaternion (0, 0, 1, 0);

			return true;
		}
		return false;
	}

	private void Update(){
		
		//Debug.Log (gyroEnabled);
		if (gyroEnabled) {
			//Debug.Log (gyro.attitude);
			transform.localRotation = GyroToUnity(Input.gyro.attitude) * rot;
		}
	}

	private static Quaternion GyroToUnity(Quaternion q){
		//Debug.Log (q);
		return new Quaternion (q.x, q.z, -q.y, -q.w);
	}
}