﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour {

	public void OnClick() {
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
}
