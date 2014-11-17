﻿using UnityEngine;
using System.Collections;

public class PlayerNetwork : Photon.MonoBehaviour {
	
	PlayerMovementController controllerScript;
	public Camera playerCamera;

	void Awake()
	{
		controllerScript = GetComponent<PlayerMovementController>();
		
		if (photonView.isMine)
		{
			//MINE: local player, simply enable the local scripts
			controllerScript.enabled = true;
			playerCamera.enabled = true;
		}
		else
		{           
			controllerScript.enabled = false;
			playerCamera.enabled = false;
		}
		
		gameObject.name = gameObject.name + photonView.viewID;
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			//We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation); 
		}
		else
		{
			//Network player, receive data
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}
	
	private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	
	void Update()
	{
		if (!photonView.isMine)
		{
			//Update remote player (smooth this, this looks good, at the cost of some accuracy)
			transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
		}
	}
}
