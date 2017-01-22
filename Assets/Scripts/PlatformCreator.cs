using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {

	public Transform floorPrefab;
	public Transform jumpPrefab;

	public int floorWidthPieces = 10;
	public int renderFloorForwardPieces = 50;

	public bool guardFloat = false;
	public bool noHoles = false;

	public UnityEngine.UI.Text pointsText;

	public TextAsset[] pieceCombinations;

	private GameManager gameManager;

	private Dictionary<char, List<Transform>> renderedPieces = new Dictionary<char, List<Transform>>();
	private Dictionary<char, List<Transform>> idlePieces = new Dictionary<char, List<Transform>>();
	private int currentOffset = 0;
	private int renderedUntil = -1;
	private int lastBlockRenderEnded = 0;

	private string[] currentPieceCombination = new string[]{};

	public void Start () {
		transform.localEulerAngles = Vector3.forward * 0.5f; // Fixes flickering in the start
		InvokeRepeating ("ResetCameraPosition", 0.3f, 0.3f);
		RenderPlatformPieces (0, renderFloorForwardPieces);
		gameManager = GameObject.FindObjectOfType<GameManager> ();
	}

	// Determine if hole or not
	bool NotInStartArea(int x, int z) {
		return z > 10;
	}

	bool RandomHole(int x, int z) {
		if (noHoles)
			return false;
		
		return GetPieceType (x, z) == ' ';
	}

	char GetPieceType(int x, int z) {
		if (noHoles)
			return ' ';

		if (!NotInStartArea (x, z)) {
			return '0';
		}

		int index = z - lastBlockRenderEnded;
		if (index >= currentPieceCombination.Length) {
			lastBlockRenderEnded = z;
			currentPieceCombination = pieceCombinations [Random.Range (0, pieceCombinations.Length)].text.Split('\n');
			return GetPieceType (x, z);
		}

		int reverse = currentPieceCombination.Length - 1 - index;
		return currentPieceCombination [reverse][floorWidthPieces-1-x];
	}

	// Render new pieces for the platform
	public void RenderPlatformPieces(int from, int to) {
		Vector3 currentPosition = this.transform.position;
		float startX = currentPosition.x - (floorWidthPieces * 0.5f);
		float startZ = currentPosition.z;
		float yPos = currentPosition.y;

		for (int z = Mathf.Max(renderedUntil + 1, from); z < to; z++) {
			for (int x = 0; x < floorWidthPieces; x++) {
				if (NotInStartArea (x, z) && RandomHole (x, z)) {
					continue;
				}

				Transform newPiece = GetNewPiece(GetPieceType(x, z));
				newPiece.SetParent (this.transform);

				PlatformPieceController platformPieceController = newPiece.gameObject.GetComponent<PlatformPieceController> ();

				platformPieceController.ResetLights ();
				if (x == 0 || NotInStartArea(x,z) && RandomHole (x-1, z)) {
					platformPieceController.ActivateLight (PlatformLight.Left);
				}
				if (x == floorWidthPieces - 1 || NotInStartArea(x,z) && RandomHole (x+1, z)) {
					platformPieceController.ActivateLight (PlatformLight.Right);
				}
				if (z >= lastBlockRenderEnded + 1 && NotInStartArea(x,z) && RandomHole (x, z - 1)) {
					platformPieceController.ActivateLight (PlatformLight.Bottom);
				}
				if (z < lastBlockRenderEnded + currentPieceCombination.Length - 1 && NotInStartArea(x,z) && RandomHole (x, z + 1)) {
					platformPieceController.ActivateLight (PlatformLight.Top);
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


		pointsText.text = currentOffset * -17 + "";
		gameManager.currentScore = currentOffset * -17;

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
		foreach (KeyValuePair<char, List<Transform>> l in renderedPieces) {
			foreach (Transform t in l.Value.ToArray()) {
				if (guardFloat) {
					Vector3 newPos = t.localPosition;
					newPos.z -= offset;
					t.localPosition = newPos;
				}


				if (t.position.z > cameraTransform.position.z) {
					DestroyPiece (t);
				}
			}
		}

		RenderPlatformPieces (-currentOffset, -currentOffset + renderFloorForwardPieces);
	}

	// Single piece lifecycle, should probaly refactor these to their own objet at some point
	private Transform GetNewPiece(char pieceType) {
		Transform newPiece;

		if (!idlePieces.ContainsKey(pieceType) || idlePieces[pieceType].Count == 0) {
			newPiece = GameObject.Instantiate (PrefabForPieceType(pieceType));
			newPiece.gameObject.name = pieceType.ToString();
		} else {
			newPiece = idlePieces[pieceType] [idlePieces[pieceType].Count - 1];
			idlePieces[pieceType].RemoveAt(idlePieces[pieceType].Count - 1);
			newPiece.GetComponentInChildren<Renderer> ().enabled = true;
		}

		if (!renderedPieces.ContainsKey(pieceType)) {
			renderedPieces [pieceType] = new List<Transform> ();
		}
		renderedPieces [pieceType].Add (newPiece);
		return newPiece;
	}

	Transform PrefabForPieceType(char pieceType) {
		switch (pieceType) {
			case '0':
				return floorPrefab;
			case '1':
				return jumpPrefab;
			default:
				return floorPrefab;
		}
	}

	private void DestroyPiece(Transform piece) {
		char pieceType = piece.gameObject.name [0];

		if (!idlePieces.ContainsKey(pieceType)) {
			idlePieces [pieceType] = new List<Transform> ();
		}

		renderedPieces[pieceType].Remove (piece);
		idlePieces[pieceType].Add (piece);
		piece.GetComponentInChildren<Renderer> ().enabled = false;
	}
}
