using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OoLaLaWaves : MonoBehaviour {

	int maxBlocks = 50;
	int startEffectFrom = 10;
	float amplitude = 5.0f;
	float spread = 10.0f;
	
	void Update () {
		Vector3 parentLocalPosition = transform.parent.localPosition;
		Vector3 localPosition = Vector3.zero;
		float distanceToCamera = Mathf.Abs (parentLocalPosition.z - Camera.main.transform.position.z);
		float multiplier = Mathf.Max(0, (distanceToCamera - startEffectFrom) / (maxBlocks - startEffectFrom));
		localPosition.y = Mathf.Lerp(0, Mathf.Sin (distanceToCamera + parentLocalPosition.x) * amplitude, multiplier);
		float direction = Mathf.FloorToInt (parentLocalPosition.x % 2) == 0 ? -1 : 1;
		localPosition.x = Mathf.Lerp(0, multiplier * parentLocalPosition.x * spread * direction, multiplier);
		transform.localPosition = localPosition;
	}
}
