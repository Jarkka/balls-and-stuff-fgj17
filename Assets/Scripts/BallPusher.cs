using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPusher : MonoBehaviour {

	public float constantPushForce = 5;
	public float boardXExtraForce;

	private Rigidbody myRigidbody;
	private SoundController soundController;
	private float startMagnitude;

	private float crashThreshold = 3;

	void Awake() {
		this.myRigidbody = this.GetComponent<Rigidbody> ();

		startMagnitude = myRigidbody.velocity.sqrMagnitude;
		crashThreshold *= crashThreshold; 
	}

	void Start() {
		soundController = GameObject.FindObjectOfType<SoundController> ();
	}

	void FixedUpdate () {
		//this.myRigidbody.AddForce (Vector3.forward * -(this.constantPushForce + (Time.frameCount * 0.0001f)));
		Vector3 newVelocity = myRigidbody.velocity;
		newVelocity.z = -this.constantPushForce;
		newVelocity.x = newVelocity.x + boardXExtraForce;
		myRigidbody.velocity = newVelocity;

		if (startMagnitude - myRigidbody.velocity.sqrMagnitude > crashThreshold) {
			soundController.PlayBallHitSoundAtPosition (transform.position);		
		}

		startMagnitude = myRigidbody.velocity.sqrMagnitude;
	}
}
