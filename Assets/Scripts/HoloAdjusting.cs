using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloAdjusting : MonoBehaviour {

	public Camera Camera_Up;
	public Camera Camera_Down;
	public Camera Camera_Left;
	public Camera Camera_Right;
	public Camera Camera_Center;

	float affect_PosValue = 0.2f;
	float affect_FieldValue = 5f;
	float affect_RotateValue = 0.3f;

	enum Move_Direct {
		Right,
		Left,
		Up,
		Down
	}

	enum Rotate_Direct {
		Plus,
		Minus
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			ModifyCameraRect (ref Camera_Up,Move_Direct.Right);
			ModifyCameraRect (ref Camera_Down,Move_Direct.Right);
			ModifyCameraRect (ref Camera_Left,Move_Direct.Right);
			ModifyCameraRect (ref Camera_Right,Move_Direct.Right);
			ModifyCameraRect (ref Camera_Center,Move_Direct.Right);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			ModifyCameraRect (ref Camera_Up,Move_Direct.Left);
			ModifyCameraRect (ref Camera_Down,Move_Direct.Left);
			ModifyCameraRect (ref Camera_Left,Move_Direct.Left);
			ModifyCameraRect (ref Camera_Right,Move_Direct.Left);
			ModifyCameraRect (ref Camera_Center,Move_Direct.Left);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			ModifyCameraRect (ref Camera_Up,Move_Direct.Up);
			ModifyCameraRect (ref Camera_Down,Move_Direct.Up);
			ModifyCameraRect (ref Camera_Left,Move_Direct.Up);
			ModifyCameraRect (ref Camera_Right,Move_Direct.Up);
			ModifyCameraRect (ref Camera_Center,Move_Direct.Up);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			ModifyCameraRect (ref Camera_Up,Move_Direct.Down);
			ModifyCameraRect (ref Camera_Down,Move_Direct.Down);
			ModifyCameraRect (ref Camera_Left,Move_Direct.Down);
			ModifyCameraRect (ref Camera_Right,Move_Direct.Down);
			ModifyCameraRect (ref Camera_Center,Move_Direct.Down);
		}
		if (Input.GetKey (KeyCode.KeypadPlus)) {
			Camera_Up.fieldOfView += affect_FieldValue * Time.deltaTime;
			Camera_Down.fieldOfView += affect_FieldValue * Time.deltaTime;
			Camera_Left.fieldOfView += affect_FieldValue * Time.deltaTime;
			Camera_Right.fieldOfView += affect_FieldValue * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.KeypadMinus)) {
			Camera_Up.fieldOfView -= affect_FieldValue * Time.deltaTime;
			Camera_Down.fieldOfView -= affect_FieldValue * Time.deltaTime;
			Camera_Left.fieldOfView -= affect_FieldValue * Time.deltaTime;
			Camera_Right.fieldOfView -= affect_FieldValue * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.KeypadMultiply)) {
			ModifyCameraRotate (ref Camera_Up, Rotate_Direct.Plus);
			ModifyCameraRotate (ref Camera_Down, Rotate_Direct.Plus);
			ModifyCameraRotate (ref Camera_Left, Rotate_Direct.Plus);
			ModifyCameraRotate (ref Camera_Right, Rotate_Direct.Plus);

		}
		if (Input.GetKey (KeyCode.KeypadDivide)) {
			ModifyCameraRotate (ref Camera_Up, Rotate_Direct.Minus);
			ModifyCameraRotate (ref Camera_Down, Rotate_Direct.Minus);
			ModifyCameraRotate (ref Camera_Left, Rotate_Direct.Minus);
			ModifyCameraRotate (ref Camera_Right, Rotate_Direct.Minus);
		}
	}

	void ModifyCameraRotate(ref Camera cam, Rotate_Direct rot_dir){
		Vector3 eluA = cam.transform.localEulerAngles;
		switch (rot_dir) {
		case Rotate_Direct.Plus:
			cam.transform.localEulerAngles = new Vector3 (eluA.x + affect_RotateValue, eluA.y, eluA.z);
			break;
		case Rotate_Direct.Minus:
			cam.transform.localEulerAngles = new Vector3 (eluA.x - affect_RotateValue, eluA.y, eluA.z);
			break;
		}

	}



	void ModifyCameraRect(ref Camera cam, Move_Direct move_dir){
		Rect tempRect = cam.rect;

		switch (move_dir) {
		case Move_Direct.Right:
			tempRect.x += affect_PosValue * Time.deltaTime;
			break;
		case Move_Direct.Left:
			tempRect.x -= affect_PosValue * Time.deltaTime;
			break;
		case Move_Direct.Up:
			tempRect.y += affect_PosValue * Time.deltaTime;
			break;
		case Move_Direct.Down:
			tempRect.y -= affect_PosValue * Time.deltaTime;
			break;
		}
		cam.rect = tempRect;
	}
}
