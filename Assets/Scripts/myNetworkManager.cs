using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
 
public class myNetworkManager : NetworkManager {
 
    public Button buttonPlayer1;
	public Button buttonPlayer2;
	public Button buttonPlayer3;
	public Button buttonMaze;
	public Button buttonHologram;

    int avatarIndex = 0;
 
    public Canvas characterSelectionCanvas;

	int usePort = 25500;

	public Text MyIPTextObj;
	public InputField ToIPTextObj;

	public void StartHologram(){
		if (!NetworkClient.active && !NetworkServer.active && NetworkManager.singleton.matchMaker == null) {
			NetworkManager.singleton.networkPort = usePort;
			NetworkManager.singleton.StartHost ();
		}
	}

	public void JoinHologram(){
		NetworkManager.singleton.networkAddress = ToIPTextObj.text;
		NetworkManager.singleton.networkPort = usePort;
		NetworkManager.singleton.StartClient ();
	}

	public string LocalIPAddress()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
				break;
			}
		}
		return localIP;
	}
 
    // Use this for initialization
    void Start () {
		MyIPTextObj.text = LocalIPAddress ();

        buttonPlayer1.onClick.AddListener (delegate {AvatarPicker ("P1");});
        buttonPlayer2.onClick.AddListener (delegate {AvatarPicker ("P2");});
		buttonPlayer3.onClick.AddListener (delegate {AvatarPicker ("P3");});
		buttonMaze   .onClick.AddListener (delegate {AvatarPicker ("Maze");});
		buttonHologram.onClick.AddListener (delegate {AvatarPicker ("Hologram");});
		// Default is Hologram
		playerPrefab = spawnPrefabs[4];

		/*
		buttonPlayer1.onClick.AddListener(delegate {JoinHologram();});
		buttonPlayer2.onClick.AddListener(delegate {JoinHologram();});
		buttonPlayer3.onClick.AddListener(delegate {JoinHologram();});
		buttonMaze.onClick.AddListener(delegate {JoinHologram();});
		buttonHologram.onClick.AddListener(delegate {StartHologram();});
		*/

		ToIPTextObj.text = PlayerPrefs.GetString ("ToIP", "localhost");
    }
 
    void AvatarPicker(string buttonName)
    {
        switch (buttonName)
        {

        case "P1":
			Debug.Log("selected player1");
            avatarIndex = 0;
            break;
        case "P2":
			Debug.Log("selected player2");
            avatarIndex = 1;
            break;
		case "P3":
			Debug.Log("selected player3");
			avatarIndex = 2;
			break;
		case "Maze":
			Debug.Log("selected maze");
			avatarIndex = 3;
			break;
		case "Hologram":
			Debug.Log("selected hologram");
			avatarIndex = 4;
			break;
        }
 
        playerPrefab = spawnPrefabs[avatarIndex];
    }
 
    /// Copied from Unity's original NetworkManager script except where noted
    public override void OnClientConnect(NetworkConnection conn)
    {
        /// ***
        /// This is added:
        /// First, turn off the canvas...
		characterSelectionCanvas.gameObject.SetActive(false);
        /// Can't directly send an int variable to 'addPlayer()' so you have to use a message service...
        IntegerMessage msg = new IntegerMessage(avatarIndex);
        /// ***
 
        if (!clientLoadedScene)
        {
            // Ready/AddPlayer is usually triggered by a scene load completing. if no scene was loaded, then Ready/AddPlayer it here instead.
            ClientScene.Ready(conn);
            if (autoCreatePlayer)
            {
                ///***
                /// This is changed - the original calls a differnet version of addPlayer
                /// this calls a version that allows a message to be sent
                ClientScene.AddPlayer(conn, 0, msg);
            }
        }
 
    }
 
    /// Copied from Unity's original NetworkManager 'OnServerAddPlayerInternal' script except where noted
    /// Since OnServerAddPlayer calls OnServerAddPlayerInternal and needs to pass the message - just add it all into one.
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        /// *** additions
        /// I skipped all the debug messages...
        /// This is added to recieve the message from addPlayer()...
        int id = 0;
 
        if (extraMessageReader != null)
        {
            IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
            id = i.value;
        }
 
        /// using the sent message - pick the correct prefab
        GameObject playerPrefab = spawnPrefabs[id];
        /// *** end of additions
 
        GameObject player;
        Transform startPos = GetStartPosition();
        if (startPos != null)
        {
            player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
        }
        else
        {
            player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
 
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}