using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour {
	public GameManager gameManager;

	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}

	void OnCollisionEnter (Collision c) {
		if (c.collider.gameObject.GetComponent<BallPusher> () != null) {
			GameObject.Destroy (c.collider.gameObject);

			if (Object.FindObjectsOfType<BallPusher> ().Length == 1) {
				gameObject.GetComponent<PauseGame> ().OnButtonClick ();
				gameManager.GameLost ();
			}
		}
	}
}
