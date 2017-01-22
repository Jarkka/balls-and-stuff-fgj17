using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

	public UnityEngine.UI.MaskableGraphic[] fadeTheseOut;
	public UnityEngine.UI.MaskableGraphic[] fadeTheseIn;
	public UnityEngine.UI.Button[] buttons;
	public UnityEngine.UI.Button[] disableButtons;

	public Sprite pauseImage;
	public Sprite playImage;

	public void Awake() {
		foreach (MaskableGraphic r in fadeTheseIn) {
			r.CrossFadeAlpha (0.0f, 0.0f, true);
		}
		foreach (Button r in buttons) {
			r.interactable = false;
		}
	}

	public void OnButtonClick() {
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			Camera.main.GetComponent<Animator> ().CrossFade("PauseMenuBlurRev", 0.6f);
			foreach (Button r in buttons) {
				r.interactable = false;
			}
			foreach (MaskableGraphic r in fadeTheseOut) {
				r.CrossFadeAlpha (1.0f, 0.6f, true);
			}
			foreach (MaskableGraphic r in fadeTheseIn) {
				r.CrossFadeAlpha (0.0f, 0.6f, true);
			}
		} else {
			Time.timeScale = 0;
			Camera.main.GetComponent<Animator> ().CrossFade("PauseMenuBlur", 0.6f);
			foreach (Button r in buttons) {
				r.interactable = true;
			}
			foreach (MaskableGraphic r in fadeTheseOut) {
				r.CrossFadeAlpha (0.0f, 0.6f, true);
			}
			foreach (MaskableGraphic r in fadeTheseIn) {
				r.CrossFadeAlpha (1.0f, 0.6f, true);
			}
			foreach (Button r in disableButtons) {
				r.interactable = false;
			}
		}

		UpdateButtonImage ();
	}

	void UpdateButtonImage() {
		if (pauseImage != null && playImage != null) {
			if (Time.timeScale == 0) {
				gameObject.GetComponent<Image> ().sprite = playImage;
			} else {
				gameObject.GetComponent<Image> ().sprite = pauseImage;
			}	
		}
	}
}
