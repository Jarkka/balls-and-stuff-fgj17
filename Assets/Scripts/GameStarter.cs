using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {
	public bool fromInput = true;
	public bool startingPrevented = true;

	void Update () {
		if (!startingPrevented && fromInput && Input.anyKeyDown) {
			StartGame ();
		}
	}

	public void StartGame() {
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}
}
