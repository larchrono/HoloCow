using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Gyro : NetworkBehaviour {

	private Quaternion rot = new Quaternion (0, 0, 1, 0);

	private void Start(){
		Debug.Log (SystemInfo.supportsGyroscope);
		transform.rotation = Quaternion.Euler (90f, 90f, 0f);
	}

	private void Update(){
		if (!isLocalPlayer)
		{
			return;
		}
		// if is player
		if (SystemInfo.supportsGyroscope) {
			//transform.localRotation = GyroToUnity (Input.gyro.attitude);
			Quaternion tempQuat = GyroToUnity (Input.gyro.attitude) * rot;
			Vector3 quaVec = tempQuat.eulerAngles;
			transform.localRotation = Quaternion.Euler(new Vector3(quaVec.x, 0 , quaVec.z));
		}
	}

	private Quaternion GyroToUnity(Quaternion q)
	{
		//return new Quaternion(q.x, q.y, -q.z, -q.w) * rot;
		return new Quaternion (q.x, q.z, -q.y, -q.w);
	}

	protected void OnGUI()
	{
		GUI.skin.label.fontSize = Screen.width / 40;

		GUILayout.Label("Orientation: " + Screen.orientation);
		GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
		GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
	}

}