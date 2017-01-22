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
		pauseButton.SetActive (false);
		gameIsOver = true;
	}
}
