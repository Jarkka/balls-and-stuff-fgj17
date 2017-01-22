using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenFader : MonoBehaviour {

	public static bool splashScreenShown = false;
	public Image myImage;
	public GameStarter gameStarter;

	// Use this for initialization
	void Start () {
		myImage = gameObject.GetComponent<Image> ();

		if (!splashScreenShown) {
			StartCoroutine (FadeSplash ());
		} else {
			Destroy (myImage.gameObject);
			gameStarter.startingPrevented = false;
		}
	}

	IEnumerator FadeSplash() {
		yield return new WaitForSeconds (3);
		myImage.CrossFadeAlpha (0, 0.3f, true);
		yield return new WaitForSeconds (0.3f);
		Destroy (myImage.gameObject);
		gameStarter.startingPrevented = false;
		splashScreenShown = true;
	}
}
