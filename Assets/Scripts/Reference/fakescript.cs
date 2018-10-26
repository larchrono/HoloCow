using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakescript : MonoBehaviour {
	private bool gyroEnabled;
	private Gyroscope gyro;

	public GameObject cube;
	private Quaternion rot;
	// Use this for initialization
	private void Start(){
		cube.transform.position = transform.position;
		gyroEnabled = EnableGyro ();
	}
	private bool EnableGyro(){
		Debug.Log (SystemInfo.supportsGyroscope);
		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;
			cube.transform.rotation = Quaternion.Euler (90f, 90f, 0f);
			rot = new Quaternion (0, 0, 1, 0);
			print (Input.gyro.attitude);

			return true;
		}
		return false;
	}
	
	// Update is called once per frame
	private void Update(){

		if (gyroEnabled) {
			transform.localRotation = GyroToUnity(Input.gyro.attitude) * rot;
			print (Input.gyro.attitude);
		}
	}

	private static Quaternion GyroToUnity(Quaternion q){
		return new Quaternion (q.x, q.z, -q.y, -q.w);
	}
}
