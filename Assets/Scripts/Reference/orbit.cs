using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Rotate (0,300*Time.deltaTime,0);
	}
}
