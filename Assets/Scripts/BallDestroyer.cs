using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour {
	void OnCollisionEnter (Collision c) {
		if (c.collider.gameObject.GetComponent<BallPusher> () != null) {
			GameObject.Destroy (c.collider.gameObject);

			if (Object.FindObjectsOfType<BallPusher> ().Length == 1) {
				GetComponent<PauseGame> ().OnButtonClick ();
			}
		}
	}
}
