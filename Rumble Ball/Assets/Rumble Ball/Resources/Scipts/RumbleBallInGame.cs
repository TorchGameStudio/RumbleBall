﻿using UnityEngine;
using System.Collections;

public class RumbleBallInGame : Photon.MonoBehaviour {

	public Transform playerPrefab;

	public void Awake()
	{
		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(RumbleBallMenu.SceneNameMenu);
			return;
		}

		Debug.Log ("RUMBLEBALL GAME AWAKE");
		// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
		PhotonNetwork.Instantiate(this.playerPrefab.name, transform.position, Quaternion.identity, 0);
	}

	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);
		
		string message;
		InRoomChat chatComponent = GetComponent<InRoomChat>();  // if we find a InRoomChat component, we print out a short message
		
		if (chatComponent != null)
		{
			// to check if this client is the new master...
			if (player.isLocal)
			{
				message = "You are Master Client now.";
			}
			else
			{
				message = player.name + " is Master Client now.";
			}
			
			
			chatComponent.AddLine(message); // the Chat method is a RPC. as we don't want to send an RPC and neither create a PhotonMessageInfo, lets call AddLine()
		}
	}
	
	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		
		// back to main menu        
		Application.LoadLevel(RumbleBallMenu.SceneNameMenu);
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		
		// back to main menu        
		Application.LoadLevel(RumbleBallMenu.SceneNameMenu);
	}
	
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}
	
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}
	
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}
	
	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
		
		// back to main menu        
		Application.LoadLevel(RumbleBallMenu.SceneNameMenu);
	}
}
