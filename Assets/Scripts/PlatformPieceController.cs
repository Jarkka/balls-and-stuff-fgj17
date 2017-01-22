using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformLight {
	Left,
	Right,
	Bottom,
	Top
}

public class PlatformPieceController : MonoBehaviour {
	public GameObject leftLightObject;
	public GameObject rightLightObject;
	public GameObject bottomLightObject;
	public GameObject topLightObject;

	public void ResetLights () {
		leftLightObject.SetActive (false);
		rightLightObject.SetActive (false);
		bottomLightObject.SetActive (false);
		topLightObject.SetActive (false);
	}

	public void ActivateLight(PlatformLight platformLight) {
		switch (platformLight) {
		case PlatformLight.Left:
			rightLightObject.SetActive (true);
			break;
		case PlatformLight.Right:
			leftLightObject.SetActive (true);
			break;
		case PlatformLight.Bottom:
			topLightObject.SetActive (true);
			break;
		case PlatformLight.Top:
			bottomLightObject.SetActive (true);
			break;
		}
	}
}
