using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpper : MonoBehaviour {

	Rigidbody myRigidbody;

	void Awake() {
		myRigidbody = this.GetComponent<Rigidbody> ();
	}

	void Update () {
		if (myRigidbody.IsSleeping())
			myRigidbody.WakeUp();

	}
}
