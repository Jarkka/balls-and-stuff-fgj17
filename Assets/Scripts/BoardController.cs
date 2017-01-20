using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

	float[] xLimits = new float[2] { -0.25f, 0.25f };

	void Start () {
		// Do da start
	}

	void FixedUpdate () {
		float hor = Input.GetAxis ("Horizontal");
		if (hor != 0) {
			float doRotate = 0;
			if (hor > 0 && gameObject.transform.localRotation.z < xLimits [1]) {
				doRotate = 1;
			}

			if (hor < 0 && gameObject.transform.localRotation.z > xLimits [0]) {
				doRotate = 1;
			}

			RotateBoard (hor * doRotate);
		}
	}

	void RotateBoard(float axisValue) {
		transform.Rotate(Vector3.forward * axisValue);
	}
}
