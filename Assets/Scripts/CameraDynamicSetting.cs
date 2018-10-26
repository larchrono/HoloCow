using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamicSetting : MonoBehaviour {

	public string CamName;

	string keyX;
	string keyY;
	string keyZ;
	string keyRx;
	string keyFov;

	Camera cam;


	void Awake(){
		cam = GetComponent<Camera>();
	}

	public void ClearKey(){
		PlayerPrefs.DeleteKey (keyX);
		PlayerPrefs.DeleteKey (keyY);
		PlayerPrefs.DeleteKey (keyZ);
		PlayerPrefs.DeleteKey (keyRx);
		PlayerPrefs.DeleteKey (keyFov);

		Debug.Log ("Clear key:" + keyY);

	}

	// Use this for initialization
	void Start () {

		keyX = CamName + "X";
		keyY = CamName + "Y";
		keyZ = CamName + "Z";
		keyRx = CamName + "Rx";
		keyFov = CamName + "Fov";

		if (!PlayerPrefs.HasKey (keyX)) {

			PlayerPrefs.SetFloat (keyX, cam.transform.localPosition.x);
			PlayerPrefs.SetFloat (keyY, cam.transform.localPosition.y);
			PlayerPrefs.SetFloat (keyZ, cam.transform.localPosition.z);
			PlayerPrefs.SetFloat (keyRx, cam.transform.localRotation.eulerAngles.x);
			PlayerPrefs.SetFloat (keyFov, cam.fieldOfView);

		}
	}

	public void SliderX(float src){
		Vector3 ov = cam.transform.localPosition;
		cam.transform.localPosition = new Vector3 (src, ov.y, ov.z);
		PlayerPrefs.SetFloat (keyX, src);
	}

	public void SliderY(float src){
		Vector3 ov = cam.transform.localPosition;
		cam.transform.localPosition = new Vector3 (ov.x, src, ov.z);
		PlayerPrefs.SetFloat (keyY, src);
	}

	public void SliderZ(float src){
		Vector3 ov = cam.transform.localPosition;
		cam.transform.localPosition = new Vector3 (ov.x, ov.y, src);
		PlayerPrefs.SetFloat (keyZ, src);
	}

	public void SliderRx(float src){
		Vector3 ov = cam.transform.localRotation.eulerAngles;
		cam.transform.localRotation = Quaternion.Euler (new Vector3 (src, ov.y, ov.z));
		PlayerPrefs.SetFloat (keyRx, src);
	}

	public void SliderFoV(float src){
		cam.fieldOfView = src;
		PlayerPrefs.SetFloat (keyFov, src);
	}

	public void ShowAllValue(){
		string msg = "x:" + cam.transform.localPosition.x + " , y:" + cam.transform.localPosition.y + " , z:" + cam.transform.localPosition.z + " , Rx:" + cam.transform.localRotation.eulerAngles.x +
		             " , FoV:" + cam.fieldOfView;
		DebugInterface.DebugMsg (msg);
	}
}
