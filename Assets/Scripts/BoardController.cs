using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

	float[] xLimits = new float[2] { -0.2f, 0.2f };
	Vector3 rotation = Vector3.zero;

	public bool enableVertical = false;

	void Start () {
		// Do da start
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void FixedUpdate () {
		RotateFromInput ("Horizontal", Vector3.forward);
		if (!this.enableVertical) {
			return;
		}
		RotateFromInput ("Vertical", -Vector3.left);
	}

	void RotateFromInput(string input, Vector3 axis) {

		float hor = Input.GetAxis (input);
		if (hor != 0) {
			float doRotate = 0;

			if (hor > 0 && gameObject.transform.localRotation.z < xLimits [1]) {
				doRotate = 1;
			}

			if (hor < 0 && gameObject.transform.localRotation.z > xLimits [0]) {
				doRotate = 1;
			}

			RotateBoard (hor * doRotate * axis);

			GameObject ball = GameObject.FindGameObjectWithTag ("DasBall");
			if (ball) {
				BallPusher ballPusher = ball.GetComponent<BallPusher> ();
				ballPusher.boardXExtraForce = -hor * 0.05f;
			}
		}
	}

	void RotateBoard(Vector3 axisValue) {
		this.rotation += axisValue;
		transform.localEulerAngles = this.rotation;
	}
}
