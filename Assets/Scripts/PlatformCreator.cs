using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {

	public Transform floorPrefab;

	public int floorWidthPieces = 10;
	public int renderFloorForwardPieces = 50;

	private List<Transform> renderedPieces = new List<Transform>();
	private List<Transform> idlePieces = new List<Transform>();
	private int currentOffset = 0;
	private int renderedUntil = -1;

	bool NotInStartArea(int x, int z) {
		return z > 10;
	}

	bool RandomHole(int z) {
		return Random.Range (0.0f, 1.0f) < z * 0.001f;
	}

	public void Start () {
		InvokeRepeating ("ResetCameraPosition", 0.3f, 0.3f);
		RenderPlatformPieces (0, renderFloorForwardPieces);
	}

	public void RenderPlatformPieces(int from, int to) {
		Vector3 currentPosition = this.transform.position;
		float startX = currentPosition.x - (floorWidthPieces * 0.5f);
		float startZ = currentPosition.z;
		float yPos = currentPosition.y;

		for (int z = Mathf.Max(renderedUntil+1, from); z < to; z++) {
			for (int x = 0; x < floorWidthPieces; x++) {
				if (NotInStartArea (x, z) && RandomHole (z)) {
					continue;
				}

				Transform newPiece = GetNewPiece();
				newPiece.SetParent (this.transform);
				newPiece.localPosition = new Vector3 (startX + x, yPos, startZ - z - currentOffset);
				newPiece.localRotation = Quaternion.identity;
			}
			renderedUntil = z;
		}
	}

	public void ResetCameraPosition() {
		Transform cameraTransform = Camera.main.transform;
		int offset = Mathf.FloorToInt (cameraTransform.position.z);
		if (offset == 0) {
			return;
		}

		currentOffset += offset;

		Transform[] allObjects = FindObjectsOfType<Transform> ();
		foreach (Transform t in allObjects) {
			if (t.parent != null || t == transform) {
				continue;
			}

			Vector3 newPos = t.position;
			newPos.z -= offset;
			t.position = newPos;
		}
			
		List<Transform> toDelete = new List<Transform> ();
		foreach (Transform t in renderedPieces) {
			Vector3 newPos = t.localPosition;
			newPos.z -= offset;
			t.localPosition = newPos;

			if (t.position.z > 3) {
				toDelete.Add(t);
			}
		}

		foreach (Transform t in toDelete) {
			DestroyPiece (t);
		}

		RenderPlatformPieces (-currentOffset, -currentOffset + renderFloorForwardPieces);
	}

	private Transform GetNewPiece() {
		Transform newPiece;

		if (idlePieces.Count == 0) {
			newPiece = GameObject.Instantiate (floorPrefab);
		} else {
			newPiece = idlePieces [idlePieces.Count - 1];
			idlePieces.RemoveAt(idlePieces.Count - 1);
			newPiece.GetComponent<Renderer> ().enabled = true;
		}
		renderedPieces.Add (newPiece);
		return newPiece;
	}

	private void DestroyPiece(Transform piece) {
		renderedPieces.Remove (piece);
		idlePieces.Add (piece);
		piece.GetComponent<Renderer> ().enabled = false;
	}
}
