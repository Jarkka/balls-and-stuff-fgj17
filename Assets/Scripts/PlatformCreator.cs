using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {

	public Transform floorPrefab;

	public int floorWidthPieces = 10;
	public int renderFloorForwardPieces = 50;

	bool NotInStartArea(int x, int z) {
		return z > 10;
	}

	bool RandomHole(int z) {
		return Random.Range (0.0f, 1.0f) < z * 0.01f;
	}

	public void Start () {
		RenderPlatformPieces (0, renderFloorForwardPieces);
	}

	public void RenderPlatformPieces(int from, int to) {
		Vector3 currentPosition = this.transform.position;
		float startX = currentPosition.x - (floorWidthPieces * 0.5f);
		float startZ = currentPosition.z;
		float yPos = currentPosition.y;

		for (int z = from; z < to; z++) {
			for (int x = 0; x < floorWidthPieces; x++) {
				if (NotInStartArea (x, z) && RandomHole (z)) {
					continue;
				}

				Transform newPiece = GameObject.Instantiate (floorPrefab);
				newPiece.SetParent (this.transform);
				newPiece.transform.position = new Vector3 (startX + x, yPos, startZ - z);
			}
		}
	}
}
