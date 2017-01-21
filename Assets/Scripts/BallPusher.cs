using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPusher : MonoBehaviour {

	public float constantPushForce = 5;

	private Rigidbody myRigidbody;

	void Awake() {
		this.myRigidbody = this.GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		//this.myRigidbody.AddForce (Vector3.forward * -(this.constantPushForce + (Time.frameCount * 0.0001f)));
		Vector3 newVelocity = myRigidbody.velocity;
		newVelocity.z = -this.constantPushForce;
		myRigidbody.velocity = newVelocity;
	}
}
