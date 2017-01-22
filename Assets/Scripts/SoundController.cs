using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public GameObject ballHitSoundObject;

	public void PlayBallHitSoundAtPosition (Vector3 position) {
		Instantiate (ballHitSoundObject, position, ballHitSoundObject.transform.rotation, transform);
	}
}
