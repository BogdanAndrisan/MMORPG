using UnityEngine;
using System.Collections;
using System;

public class ConnectionScript : MonoBehaviour
{
	private string gameName = "The Game Game";
	private string serverName = "", maxPlayers = "0", port = "25420";
	private Rect windowRect = new Rect(0, 0, 400, 400);

	public bool ServerWindow = true;
	public GameObject playerPrefab;
	void Start(){
		MasterServer.ipAddress = "127.0.0.1";
	}

	private void OnGUI()
	{
		if(ServerWindow == true){
		windowRect = GUI.Window (5, windowRect, windowFunc, "Servers");
		}
		if (Network.peerType == NetworkPeerType.Disconnected)
		{
			GUILayout.Label ("Server Name");
			serverName = GUILayout.TextField (serverName);
			
			GUILayout.Label ("Port");
			port = GUILayout.TextField (port);
			
			GUILayout.Label ("Max Players");
			maxPlayers = GUILayout.TextField (maxPlayers);
			
			if ( GUILayout.Button ("Create Server"))
			{
				try
				{

					//MasterServer.port = 23466;
					//Network.InitializeSecurity();
					Network.InitializeServer(int.Parse(maxPlayers), int.Parse(port), !Network.HavePublicAddress());
					MasterServer.RegisterHost (gameName, serverName);
				}
				catch (Exception)
				{
					print ("Please Type in numbers for port and max players");
				}
			}
		}
		else
		{
			if (GUILayout.Button ("Disconnect"))
			{
				Network.Disconnect ();
	
			}
		}
	}
	
	private void windowFunc(int id)
	{
		if (GUILayout.Button ("Refresh"))
		{
			//MasterServer.ipAddress = "127.0.0.1";
			//MasterServer.port = 23466;
			MasterServer.RequestHostList (gameName);
		}
		GUILayout.BeginHorizontal ();
		
		GUILayout.Box ("Server Name");
		
		GUILayout.EndHorizontal ();
		
		if (MasterServer.PollHostList().Length != 0)
		{
			HostData[] data = MasterServer.PollHostList ();
			foreach(HostData c in data)
			{
				GUILayout.BeginHorizontal ();
				GUILayout.Box (c.gameName);
				if(GUILayout.Button ("Connect"))
				{
					Network.Connect (c);
				}
				GUILayout.EndHorizontal ();
			}
		}
		
		GUI.DragWindow (new Rect (0, 0, Screen.width, Screen.height));
	}


	void OnConnectedToServer(){
		Debug.Log ("Server Joined");
		SpawnPlayer();
		Debug.Log ("Player Spawned");
	}
	void OnServerInitialized(){
		Debug.Log("Server Initializied");
		SpawnPlayer();
		Debug.Log ("Player Spawned");
	}
	private void SpawnPlayer(){
		Network.Instantiate(playerPrefab, new Vector3(0f,5f,0f),Quaternion.identity,0);
	}
	void Update(){
		if(Input.GetKeyDown(KeyCode.V)){
			ServerWindow=!ServerWindow;
		}
	}
}
