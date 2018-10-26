using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerService : NetworkBehaviour {
	public static ServerService current;

	public Vector2 MazeAccelerate;

	void Awake(){
		current = this;
	}

	public void SetMazeAccAndSendWeb(float x, float y){
		MazeAccelerate = new Vector2 (x, y);
		MyWebSocket.current.accXToSend = x;
		MyWebSocket.current.accYToSend = y;
	}

	[ClientRpc]
	public void RpcUpdateMaze(NetworkInstanceId nid){
		GameObject newMaze = ClientScene.FindLocalObject (nid);
		LogicSystem.current.currentMaze = newMaze;
	}
}
