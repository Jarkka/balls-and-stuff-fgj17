using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJUMP : MonoBehaviour {

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.GetComponent<BallPusher> ()) {
			Rigidbody r = c.gameObject.GetComponent<Rigidbody> ();
			if (r.velocity.y < 5) {
				r.velocity += Vector3.up * 10.0f;

				c.gameObject.GetComponent<BallPusher> ().SpawnHitParticles ();

				SoundController soundController = GameObject.FindObjectOfType<SoundController> ();
				soundController.PlayBallJumpSoundAtPosition (c.gameObject.transform.position);
			}
		}
	}

}
