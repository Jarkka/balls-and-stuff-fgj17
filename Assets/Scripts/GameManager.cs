using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject musicPlayer;

	void Start () {
		if (GameObject.FindGameObjectWithTag ("MusicPlayer") == null) {
			Instantiate (musicPlayer, transform.position, transform.rotation);
		}
	}
}
