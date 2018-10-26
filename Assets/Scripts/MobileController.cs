using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour {

	public static MobileController current;
	public static bool EnabledControl;

	float Amp = 2;
	float filter = 0.4f;
	public float BallAccelerateX;
	public float BallAccelerateY;

	public float MazeRotateX;
	public float MazeRotateY;

	void Awake(){
		current = this;
	}

	// Use this for initialization
	void Start () {
		EnabledControl = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!EnabledControl)
			return;

		if (Input.acceleration.x != 0 || Input.acceleration.y != 0) {

			// Setup position by Accelerometer
			BallAccelerateX = Input.acceleration.x * Amp;
			BallAccelerateY = Input.acceleration.y * Amp;

			if (Mathf.Abs (BallAccelerateX) < filter)
				BallAccelerateX = 0;
			if (Mathf.Abs (BallAccelerateY) < filter)
				BallAccelerateY = 0;

			MazeRotateX = Input.acceleration.x;
			MazeRotateY = Input.acceleration.y;

		} else {

			// Setup position by Keyboard
			BallAccelerateX = Input.GetAxisRaw ("Horizontal") * Amp;
			BallAccelerateY = Input.GetAxisRaw ("Vertical") * Amp;

			MazeRotateX = Input.GetAxisRaw ("Horizontal");
			MazeRotateY = Input.GetAxisRaw ("Vertical");
		}
	}
}
