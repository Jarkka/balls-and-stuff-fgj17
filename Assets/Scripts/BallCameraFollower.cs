using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCameraFollower : MonoBehaviour {

	public Transform target;
	private float difference;
	private BallPusher[] balls;

	void Awake() {
		difference = this.transform.position.z - target.position.z;
		this.balls = Object.FindObjectsOfType<BallPusher> ();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.R)) {
			Time.timeScale = 1;
			Application.LoadLevel (Application.loadedLevel);
		}

		if (this.target == null) FindLeadingBall ();
		if (this.target == null)
			return;
		Vector3 newPos = this.transform.position;
		newPos.z = target.position.z + difference;
		this.transform.position = newPos;

		if (Time.frameCount % 60 == 0)
			FindLeadingBall ();
	}

	void FindLeadingBall() {
		
		BallPusher leading = null;
		foreach(BallPusher ball in balls) {
			leading = leading == null || ball != null && ball.transform.position.z < leading.transform.position.z ? ball : leading;
		}
		if (leading == null)
			return;
		this.target = leading.transform;
	}

	void FixedUpdate() {
	}
}
