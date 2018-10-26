using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSideCameraSetup : MonoBehaviour {

	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
		Matrix4x4 mat = cam.projectionMatrix;
		mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
		cam.projectionMatrix = mat;
	}

}
