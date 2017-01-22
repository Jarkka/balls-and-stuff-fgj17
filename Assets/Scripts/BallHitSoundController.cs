using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitSoundController : MonoBehaviour {

	public AudioClip[] ballHitSounds;

	// Use this for initialization
	void Start () {
		AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
		audioSource.clip = ballHitSounds [Random.Range (0, ballHitSounds.Length)];
		audioSource.Play ();
		DestroyAfterSeconds (1);
	}
	
	IEnumerator DestroyAfterSeconds(float seconds) {
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}
}
