using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCreator : MonoBehaviour {

	public Transform[] platformPiecePrefabs;
	public int widthNumPieces = 10;
	public int heightNumPieces = 10;
	public Transform wallPrefab;

	[Range(0.0f, 1.0f)]
	public float startHoleProbability = 0.5f;
	public bool enableStartArea = true;
	public bool centerAll = false;

	private Transform[] platformPieces;

	bool NotInStartArea(int x, int z) {
		return !enableStartArea || z > 10;
	}

	bool RandomHole(int z) {
		float holeProbability = z * 1.0f / heightNumPieces;
		return Random.Range (0.0f, 1.0f) < startHoleProbability + holeProbability * 0.1f;
	}

	// Use this for initialization
	public void Start () {
		Vector3 currentPosition = this.transform.position;
		float startX = currentPosition.x - (widthNumPieces * 0.5f);
		float startZ = centerAll ? currentPosition.z + (heightNumPieces * 0.5f) : currentPosition.z;
		float yPos = currentPosition.y;
			
		for (int z = 0; z < heightNumPieces; z++) {
			for (int x = 0; x < widthNumPieces; x++) {
				int prefabIndex = NotInStartArea(x,z) && RandomHole(z) ? 1 : 0;
				Transform newPiece = GameObject.Instantiate (platformPiecePrefabs [prefabIndex]);
				newPiece.SetParent (this.transform);
				newPiece.transform.position = new Vector3 (startX + x, yPos, startZ - z);
			}

			for (int x = -1; x <= widthNumPieces + 1; x += widthNumPieces + 1) {
				Transform newWall = GameObject.Instantiate (this.wallPrefab);
				newWall.SetParent (this.transform);
				newWall.transform.position = new Vector3 (startX + x, yPos, startZ - z);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
