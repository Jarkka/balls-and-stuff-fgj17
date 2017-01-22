using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPusher : MonoBehaviour {

	public float constantPushForce = 5;
	public float boardXExtraForce;

	private Rigidbody myRigidbody;
	private SoundController soundController;

	private float crashThreshold = 4;

	void Awake() {
		this.myRigidbody = this.GetComponent<Rigidbody> ();
		crashThreshold *= crashThreshold; 
	}

	void Start() {
		soundController = GameObject.FindObjectOfType<SoundController> ();
	}

	void OnCollisionEnter(Collision c) {
		if (c.impulse.sqrMagnitude > 50) {
			soundController.PlayBallHitSoundAtPosition (transform.position);
		}
	}

	void FixedUpdate () {
		//this.myRigidbody.AddForce (Vector3.forward * -(this.constantPushForce + (Time.frameCount * 0.0001f)));
		Vector3 newVelocity = myRigidbody.velocity;
		newVelocity.z = -this.constantPushForce;
		newVelocity.x = newVelocity.x + boardXExtraForce;
		myRigidbody.velocity = newVelocity;
	}
}
