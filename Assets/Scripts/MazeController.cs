using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MazeController : NetworkBehaviour {

	float amp = 40;
	Quaternion goalQuat;

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();

		MobileController.EnabledControl = true;
		if (LogicSystem.current.PlayerCamera != null)
			LogicSystem.current.PlayerCamera.SetActive (true);

		InvokeRepeating ("UpdateAcc", 0, 0.2f);

	}
		
	void Start () {
		goalQuat = transform.rotation;

		//Init Maze To correct Position
		transform.position = new Vector3 (0f, 0, 0f); //transform.position = new Vector3 (0f, -84f, 0f);

		gameObject.name = "MazeZoneNetWork (" + GetComponent<NetworkIdentity> ().netId + ")";

		if (isLocalPlayer) {
			var nid = GetComponent<NetworkIdentity> ().netId;
			CmdUpdateMaze (nid);
			DebugInterface.DebugMsg ("Cmd Maze Local Player :" + nid);
		}
	}

	/*
	protected void OnGUI()
	{
		GUI.skin.label.fontSize = Screen.width / 40;
		GUILayout.Label("Accelero: " + Input.acceleration.x + "," + Input.acceleration.y);
	}
	*/

	void Update () {
		if (!isLocalPlayer)
			return;
		
		goalQuat = Quaternion.Euler (new Vector3 (MobileController.current.MazeRotateY * amp, 0, MobileController.current.MazeRotateX * amp));
		transform.rotation = Quaternion.Lerp(transform.rotation, goalQuat, 0.15f);

	}

	[Command]
	public void CmdUpdateMaze(NetworkInstanceId nid){
		if (LogicSystem.current.currentMaze != null) {
			NetworkServer.Destroy (LogicSystem.current.currentMaze);
		}
		ServerService.current.RpcUpdateMaze (nid);
	}

	[Command]
	public void CmdRecieveMazeAccelerate(float x, float y){
		ServerService.current.SetMazeAccAndSendWeb(x, y);
	}

	void UpdateAcc(){
		CmdRecieveMazeAccelerate (MobileController.current.MazeRotateY * amp, MobileController.current.MazeRotateX * amp);
	}

}
