using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {
	void Update () {
		if (Input.anyKeyDown) {
			UnityEngine.SceneManagement.SceneManager.LoadScene (1);
		}
	}
}
