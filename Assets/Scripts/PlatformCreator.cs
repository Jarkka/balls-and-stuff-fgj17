using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {

	public Transform floorPrefab;

	public int floorWidthPieces = 10;
	public int renderFloorForwardPieces = 50;

	public bool guardFloat = false;
	public bool noHoles = false;

	public UnityEngine.UI.Text pointsText;

	private List<Transform> renderedPieces = new List<Transform>();
	private List<Transform> idlePieces = new List<Transform>();
	private int currentOffset = 0;
	private int renderedUntil = -1;

	public void Start () {
		transform.localEulerAngles = Vector3.forward * 0.5f; // Fixes flickering in the start
		InvokeRepeating ("ResetCameraPosition", 0.3f, 0.3f);
		RenderPlatformPieces (0, renderFloorForwardPieces);
	}

	// Determine if hole or not
	bool NotInStartArea(int x, int z) {
		return z > 10;
	}

	bool RandomHole(int z) {
		return !noHoles && Random.Range (0.0f, 1.0f) < z * 0.001f;
	}

	// Render new pieces for the platform
	public void RenderPlatformPieces(int from, int to) {
		Vector3 currentPosition = this.transform.position;
		float startX = currentPosition.x - (floorWidthPieces * 0.5f);
		float startZ = currentPosition.z;
		float yPos = currentPosition.y;

		for (int z = Mathf.Max(renderedUntil + 1, from); z < to; z++) {
			for (int x = 0; x < floorWidthPieces; x++) {
				if (NotInStartArea (x, z) && RandomHole (z)) {
					continue;
				}

				Transform newPiece = GetNewPiece();
				newPiece.SetParent (this.transform);

				PlatformPieceController platformPieceController = newPiece.gameObject.GetComponent<PlatformPieceController> ();
		
				if (x == 0) {
					platformPieceController.ActivateLight (PlatformLight.Left);
				} else if (x == floorWidthPieces - 1) {
					platformPieceController.ActivateLight (PlatformLight.Right);
				} else {
					platformPieceController.ResetLights ();
				}

				int guardFloatOffset = guardFloat ? currentOffset : 0;
				newPiece.localPosition = new Vector3 (startX + x, yPos, startZ - z - guardFloatOffset);
				newPiece.localRotation = Quaternion.identity;
			}
			renderedUntil = z;
		}
	}

	// Nudge everything a bit backwards to avoid float losing precision
	public void ResetCameraPosition() {
		Transform cameraTransform = Camera.main.transform;
		int offset = Mathf.FloorToInt (cameraTransform.position.z);
		if (offset == 0) {
			return;
		}

		currentOffset = guardFloat ? currentOffset + offset : offset;

		pointsText.text = currentOffset * -17 + " points";

		if (guardFloat) {
			// Nudge everything but the platform
			Transform[] allObjects = FindObjectsOfType<Transform> ();
			foreach (Transform t in allObjects) {
				if (t.parent != null || t == transform) {
					continue;
				}

				Vector3 newPos = t.position;
				newPos.z -= offset;
				t.position = newPos;
			}
		}

		// Nudge pieces in platform
		List<Transform> toDelete = new List<Transform> ();
		foreach (Transform t in renderedPieces.ToArray()) {
			if (guardFloat) {
				Vector3 newPos = t.localPosition;
				newPos.z -= offset;
				t.localPosition = newPos;
			}


			if (t.position.z > cameraTransform.position.z) {
				DestroyPiece (t);
			}
		}

		RenderPlatformPieces (-currentOffset, -currentOffset + renderFloorForwardPieces);
	}

	// Single piece lifecycle, should probaly refactor these to their own objet at some point
	private Transform GetNewPiece() {
		Transform newPiece;

		if (idlePieces.Count == 0) {
			newPiece = GameObject.Instantiate (floorPrefab);
		} else {
			newPiece = idlePieces [idlePieces.Count - 1];
			idlePieces.RemoveAt(idlePieces.Count - 1);
			newPiece.GetComponentInChildren<Renderer> ().enabled = true;
		}
		renderedPieces.Add (newPiece);
		return newPiece;
	}

	private void DestroyPiece(Transform piece) {
		renderedPieces.Remove (piece);
		idlePieces.Add (piece);
		piece.GetComponentInChildren<Renderer> ().enabled = false;
	}
}
