using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerometerMove : MonoBehaviour {
	float temp;
	public Text accelerometerX;
	// Use this for initialization
	void Start () {
		transform.Rotate (90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		/*temp = Input.acceleration.x;
		accelerometerX.text = Input.acceleration.x.ToString();
		transform.Rotate (0, 0, Input.acceleration.x);
		Debug.Log (temp);*/
	}
}
