using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoLaLaWaves : MonoBehaviour {
	
	void Update () {
		Vector3 parentLocalPosition = transform.parent.localPosition;
		Vector3 localPosition = Vector3.zero;
		float distanceToCamera = Mathf.Abs (parentLocalPosition.z - Camera.main.transform.position.z);
		float multiplier = Mathf.Max(0, (distanceToCamera - 10) / 40);
		localPosition.y = Mathf.Lerp(0, Mathf.Sin (distanceToCamera + parentLocalPosition.x) * 5.0f, multiplier);
		localPosition.x = Mathf.Lerp(0, multiplier * parentLocalPosition.x * 10.0f, multiplier);
		transform.localPosition = localPosition;
	}
}
