using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestBallMove : MonoBehaviour {
	/*
	private float speed;
	private Rigidbody rb;
	// Use this for initialization

	public Text _textLog;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		_textLog = GameObject.Find ("TextLog").GetComponent<Text>();
	}

	void Update () {

		if (Input.acceleration.x != 0 || Input.acceleration.y != 0) {

			// Setup position by Accelerometer

			if (Input.acceleration.x > 0.3) {
				parseData.accX = 5;
			} else if (Input.acceleration.x < -0.3) {
				parseData.accX = -5;
			} else {
				parseData.accX = 0;
			}

			if (Input.acceleration.y > 0.3) {
				parseData.accY = 5;
			} else if (Input.acceleration.y < -0.3) {
				parseData.accY = -5;
			} else {
				parseData.accY = 0;
			}
		} else {

			// Setup position by Keyboard

			parseData.accX = Input.GetAxisRaw ("Horizontal") * 5;

			Debug.Log ("move X" + Input.GetAxisRaw ("Horizontal"));

			parseData.accY = Input.GetAxisRaw ("Vertical") * 5;

		}

		speed = LogicSystem.current.BallMoveSpeed;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//if(Input.acceleration.x > 
		float moveHorizontal = parseData.accX * speed ;
		float moveVertical = parseData.accY * speed ;

		float gravitySpeed = rb.velocity.y;

		Vector3 movement = new Vector3 (moveHorizontal, gravitySpeed, moveVertical);

		rb.velocity = (movement);

		_textLog.text = "" + movement;
	}
	*/
}