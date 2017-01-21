using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformLight {
	Left,
	Right
}

public class PlatformPieceController : MonoBehaviour {
	public GameObject leftLightObject;
	public GameObject rightLightObject;

	public void ResetLights () {
		leftLightObject.SetActive (false);
		rightLightObject.SetActive (false);
	}

	public void ActivateLight(PlatformLight platformLight) {
		switch (platformLight) {
		case PlatformLight.Left:
			rightLightObject.SetActive (true);
			leftLightObject.SetActive (false);
			break;

		case PlatformLight.Right:
			leftLightObject.SetActive (true);
			rightLightObject.SetActive (false);
			break;
		}
	}
}
