using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject musicPlayer;
	public bool escQuitsGame;
	public PauseGame pauseGame;
	public GameObject pauseButton;
	public bool gameIsOver;
	public bool hideCursorOnStart = false;

	public int currentScore = 0;

	void Start () {
		if (GameObject.FindGameObjectWithTag ("MusicPlayer") == null) {
			Instantiate (musicPlayer, transform.position, transform.rotation);
		}

		if (hideCursorOnStart) {
			Cursor.visible = false;
		}
	}

	void Update() {
		if (!gameIsOver && Input.GetKeyDown (KeyCode.Escape)) {
			if (escQuitsGame) {
				Application.Quit();
			} else if (pauseGame != null) {
				pauseGame.OnButtonClick ();
			}
		}
	}

	public void GameLost() {
		int lastLocalHighscore = PlayerPrefs.GetInt("local-highscore", 0);
		if (currentScore > lastLocalHighscore) {
			lastLocalHighscore = currentScore;
			PlayerPrefs.SetInt ("local-highscore", currentScore);
		}
		pauseButton.SetActive (false);
		gameIsOver = true;
	}
}
