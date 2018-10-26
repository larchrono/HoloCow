using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class SerialDelegate : MonoBehaviour {

	public delegate void CoinAddIn();
	public static event CoinAddIn coinAddInEvent;

	public delegate void CloseSign (string data);
	public static event CloseSign closeSignEvent;

	public SerialPort sp_Coin = new SerialPort("COM5",9600);
	public SerialPort sp_A = new SerialPort("COM4",9600);
	public SerialPort sp_B = new SerialPort("COM3",9600);

	Thread recThread_coin;
	Thread recThread_A;
	Thread recThread_B;

	// Use this for initialization
	void Start () {
		
		sp_Coin.Open ();
		sp_A.Open ();
		sp_B.Open ();

		//sp_Coin.ReadTimeout = 5000;

		recThread_coin = new Thread (RecieveThread_Coin);
		recThread_coin.Start ();

		recThread_A = new Thread (RecieveThread_A);
		recThread_A.Start ();

		recThread_B = new Thread (RecieveThread_B);
		recThread_B.Start ();
	}

	void RecieveThread_Coin(){
		while (true) {
			if (sp_Coin.IsOpen) {
				string data = sp_Coin.ReadLine ();

				//Debug.Log (data);

				if (coinAddInEvent != null)
					coinAddInEvent ();
			}
			Thread.Sleep (10);
		}
	}

	void RecieveThread_A(){
		while (true) {
			if (sp_A.IsOpen) {
				string data = sp_A.ReadLine ();

				//Debug.Log (data);

				if (closeSignEvent != null)
					closeSignEvent (data);
			}
			Thread.Sleep (10);
		}
	}

	void RecieveThread_B(){
		while (true) {
			if (sp_B.IsOpen) {
				string data = sp_B.ReadLine ();

				//Debug.Log (data);

				if (closeSignEvent != null)
					closeSignEvent (data);
			}
			Thread.Sleep (10);
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.C)) {
			if (coinAddInEvent != null)
				coinAddInEvent ();
		}

		if (Input.GetKeyDown (KeyCode.S)) {
			if (closeSignEvent != null)
				closeSignEvent ("100");
		}
	}

	void OnApplicationQuit()
	{
		if(sp_Coin != null)
			sp_Coin.Close ();
		if(sp_A != null)
			sp_A.Close ();
		if(sp_B != null)
			sp_B.Close ();
		
		if(recThread_coin != null)
			recThread_coin.Abort ();
		if(recThread_A != null)
			recThread_A.Abort ();
		if(recThread_B != null)
			recThread_B.Abort ();
		
		Debug.Log("Application ending after " + Time.time + " seconds");

	}
}
