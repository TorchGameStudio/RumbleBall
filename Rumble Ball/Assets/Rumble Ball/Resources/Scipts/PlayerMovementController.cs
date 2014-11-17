using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {
	public float accelerationForce = 10f;
	public float rotationForce = 3f;

	void Awake () {
		Debug.Log ("PLAYER AWAKE");
	}

	void Update () {

		float rotation = Input.GetAxis("Horizontal");
		float acceleration = -Input.GetAxis("Vertical");
		
		rigidbody.AddTorque(0, rotation * rotationForce, 0);
		rigidbody.AddForce(transform.forward * acceleration * accelerationForce);

	}
}
