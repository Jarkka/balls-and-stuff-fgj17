using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBoardCreator : PlatformCreator {

	// Use this for initialization
	public void Start () {
		base.Start ();
		Vector3 currentPosition = this.transform.position;
		float startX = currentPosition.x - (widthNumPieces * 0.5f);
		float startZ = centerAll ? currentPosition.z + (heightNumPieces * 0.5f) : currentPosition.z;
		float yPos = currentPosition.y;

		for (int x = 0; x < widthNumPieces; x++) {
			for (int z = -1; z <= heightNumPieces + 1; z += heightNumPieces + 1) {
				Transform newWall = GameObject.Instantiate (this.wallPrefab);
				newWall.SetParent (this.transform);
				newWall.transform.position = new Vector3 (startX + x, yPos, startZ - z);
			}
		}
	}
}
