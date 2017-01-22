using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour {
	public GameManager gameManager;
	public UnityEngine.UI.Text highScoreText;
	public UnityEngine.UI.Text highScoreLabelText;

	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}

	void OnCollisionEnter (Collision c) {
		if (c.collider.gameObject.GetComponent<BallPusher> () != null) {
			StartCoroutine(DestroyGameObjectWithDelay (c.collider.gameObject));
		}
	}

	IEnumerator DestroyGameObjectWithDelay(GameObject obj) {
		yield return new WaitForSeconds(0.1f);
		GameObject.Destroy (obj);

		if (Object.FindObjectsOfType<BallPusher> ().Length == 1) {
			gameObject.GetComponent<PauseGame> ().OnButtonClick ();
			gameManager.GameLost ();
			int highscore = PlayerPrefs.GetInt("local-highscore", 0);
			if (highscore == gameManager.currentScore) {
				highScoreLabelText.text = "NEW LOCAL HIGHSCORE!";
			}
			highScoreText.text = highscore.ToString ();
		}
	}
}
