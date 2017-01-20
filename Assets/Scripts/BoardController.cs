using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

	void Start () {
		// Do da start
	}

	void FixedUpdate () {
		float hor = Input.GetAxis ("Horizontal");
		if (hor != 0) {
			//Debug.Log (hor);
			transform.Rotate(Vector3.forward * hor);
		}

		Debug.Log (gameObject.transform.localRotation.z);
	}
}
