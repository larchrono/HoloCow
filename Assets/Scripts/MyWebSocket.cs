using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp.Contrib;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

public class MyWebSocket : MonoBehaviour {

	public static MyWebSocket current;

	public float accXToSend = 0;
	public float accYToSend = 0;

	// Websocket server will be new thread
	WebSocketServer wsServer;

	void Awake(){
		current = this;
	}

	void Start(){
		#if UNITY_ANDROID
		DebugInterface.DebugMsg ("Android Platform");
		#elif UNITY_IOS
		DebugInterface.DebugMsg ("IOS Platform");
		#elif UNITY_STANDALONE_OSX
		DebugInterface.DebugMsg ("OSX Platform");
		#elif UNITY_STANDALONE_WIN
		DebugInterface.DebugMsg ("Windows Platform");
		SetupWebSocketServer ();
		#endif

		#if UNITY_EDITOR
		DebugInterface.DebugMsg ("Unity Editor Platform");
		SetupWebSocketServer ();
		#endif




	}

	void SetupWebSocketServer(){
		if (wsServer != null)
			return;
		wsServer = new WebSocketServer (25568);

		wsServer.AddWebSocketService<BasicWebsocketBehaviour>("/BasicWebsocketBehaviour");
		wsServer.AddWebSocketService<MobileAcc>("/mobile");

		wsServer.Start ();
		InvokeRepeating ("MyInvoke", 0, 0.05f);
		DebugInterface.DebugMsg ("Start Websocket Server");
	}

	void OnDestroy(){
		if (wsServer == null)
			return;

		wsServer.Stop ();
		Debug.Log ("destroy");
	}

	void MyInvoke(){
		string msg = "" + accXToSend + "," + accYToSend;
		wsServer.WebSocketServices.BroadcastAsync (msg, CompleteFunc);
	}

	void CompleteFunc(){
		
	}

	/*
	WebSocketServer wsServer;

	// Use this for initialization
	void Start () {
		wsServer = new WebSocketServer ();
		if(!wsServer.Setup(25568)){
			Debug.Log("cannot Setup port");
		}
		if (!wsServer.Start ()) {
			Debug.Log("cannot start server");
		}
		wsServer.NewSessionConnected += (session) => {
			var logMsg = string.Format("{0:HH:MM:ss}  client:{1} create connecting", DateTime.Now, GetSessionName(session));
			Debug.Log(logMsg);
			var msg = string.Format("{0:HH:MM:ss} {1} enter connecting", DateTime.Now, GetSessionName(session));
			SendToAll(session, msg);
		};
	}
	private string GetSessionName(WebSocketSession session)
	{
		//这里用Path来取Name 不太科学…… 
		return HttpUtility.UrlDecode(session.Path.TrimStart('/'));
	}
	void SendToAll(WebSocketSession session, string msg)
	{
		//broadcast
		foreach (var sendSession in session.AppServer.GetAllSessions())
		{
			sendSession.Send(msg);
		}
	}
	*/

}

class BasicWebsocketBehaviour : WebSocketBehavior
{
	protected override void OnMessage(MessageEventArgs e)
	{
		base.OnMessage(e);
		Debug.Log("msg: "+ e.Data);
	}
}

class MobileAcc : WebSocketBehavior
{
	protected override void OnMessage(MessageEventArgs e)
	{
		base.OnMessage(e);
		Debug.Log("msg: "+ e.Data);

	}
}