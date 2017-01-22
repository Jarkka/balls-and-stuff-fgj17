using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistObjectController : MonoBehaviour {
	void Awake() {
		DontDestroyOnLoad(gameObject);
	}
}
