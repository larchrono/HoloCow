using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class BallMoveScript : NetworkBehaviour {

	[SyncVar] Vector3 position;

	NetworkIdentity thisNID;

	Rigidbody rb;

	string movementMsg;

	float delTime = 0;
	float sendSyncRate = 0.1f;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		thisNID = GetComponent<NetworkIdentity> ();

		gameObject.name = "Player (" + thisNID.netId + ")";

		InvokeRepeating ("ShowMovement",0,0.5f);
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();

		MobileController.EnabledControl = true;
		if (LogicSystem.current.PlayerCamera != null)
			LogicSystem.current.PlayerCamera.SetActive (true);

		DebugInterface.DebugMsg ("Ball Start Local player");
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isLocalPlayer) {
			transform.position = Vector3.Lerp(transform.position, position, 0.15f);
			return;
		}
		
		//Moving Ball By Acc Controll Component
		float moveHorizontal = MobileController.current.BallAccelerateX * LogicSystem.current.BallMoveSpeed ;
		float moveVertical = MobileController.current.BallAccelerateY * LogicSystem.current.BallMoveSpeed ;

		//float gravitySpeed = rb.velocity.y;
		//Vector3 movement = new Vector3 (moveHorizontal, gravitySpeed, moveVertical);

		Vector3 movement = new Vector3 (-moveHorizontal, 0, moveVertical);

		if (movement.magnitude == 0) {
			//rb.velocity = Vector3.zero;
		} else {
			rb.AddForce (movement);
		}

		movementMsg = "" + movement;

		delTime += Time.fixedDeltaTime;
		if (delTime > sendSyncRate) {
			delTime = 0;
			CmdUpdateBallPosition (transform.position);
		}
	}

	[Command]
	public void CmdUpdateBallPosition(Vector3 src){
		position = src;
	}

	void ShowMovement(){
		//DebugInterface.DebugMsg (movementMsg);
	}

}
