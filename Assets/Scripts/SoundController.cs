using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public AudioSource musicSource;
	public GameObject ballHitSoundObject;
	public bool canPlaySounds = false;

	void Start () {
		musicSource.volume = 0;
		StartCoroutine(FadeInMusic ());
	}

	public void PlayBallHitSoundAtPosition (Vector3 position) {
		if (canPlaySounds) {
			Instantiate (ballHitSoundObject, position, ballHitSoundObject.transform.rotation, transform);
		}
	}

	IEnumerator FadeInMusic () {
		while (musicSource.volume < 1) {
			musicSource.volume = musicSource.volume + 0.3f * Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}

		canPlaySounds = true;
	}
}
