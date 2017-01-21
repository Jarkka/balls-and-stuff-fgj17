using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {
	public bool fromInput = true;

	void Update () {
		if (fromInput && Input.anyKeyDown) {
			StartGame ();
		}
	}

	public void StartGame() {
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene (1);
	}
}
