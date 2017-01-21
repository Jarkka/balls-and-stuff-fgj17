using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallCameraFollower : MonoBehaviour {

	public Transform target;
	public BallPusher[] balls;

	private float difference;
	private Vector3 startPosition;

	void Awake () {
		startPosition = this.transform.position;
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.R)) {
			Time.timeScale = 1;
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

		balls = this.balls = Object.FindObjectsOfType<BallPusher> ();
		if (balls.Length > 0) {
			if (this.target == null) FindLeadingBall ();
			if (this.target == null)
				return;

			Vector3 newPos = target.position + startPosition;
			newPos.y = startPosition.y;
			newPos.x = startPosition.x;
			transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 2f);

			if (Time.frameCount % 60 == 0)
				FindLeadingBall ();
		}
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
