using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerController : MonoBehaviour {

	public int spawnedBallAmount = 0;
	public GameObject ballPrefab;
	public GameObject model;

	private List<Vector3> reservedPositions = new List<Vector3>();
	private float margin = 1f;

	// Use this for initialization
	void Start () {
		model.GetComponent<MeshRenderer> ().enabled = false;

		for (int i = 0; i < spawnedBallAmount; i++) {
			Instantiate (ballPrefab, GetRandomVector3InsideModel (), ballPrefab.transform.rotation, transform);
		}
	}
	
	Vector3 GetRandomVector3InsideModel () {
		float[] xAxis = new float[2] {0,0};
		float[] yAxis = new float[2] {0,0};
		float[] zAxis = new float[2] {0,0};
		List<Vector3> points = new List<Vector3>();

		Mesh myMesh = model.GetComponent<MeshFilter>().mesh;

		for (var i = 0; i < myMesh.vertices.Length; i++) {
			Vector3 worldPosition = model.transform.TransformPoint (myMesh.vertices [i]);
			Vector3 vector = new Vector3 (worldPosition.x - model.transform.position.x, worldPosition.y - model.transform.position.y, worldPosition.z - model.transform.position.z);

			if (vector.x < xAxis[0]) {
				xAxis [0] = vector.x;
			} else if (vector.x > xAxis[1]) {
				xAxis [1] = vector.x;
			}

			if (vector.y < yAxis[0]) {
				yAxis [0] = vector.y;
			} else if (vector.y > yAxis[1]) {
				yAxis [1] = vector.y;
			}

			if (vector.z < zAxis[0]) {
				zAxis [0] = vector.z;
			} else if (vector.z > zAxis[1]) {
				zAxis [1] = vector.z;
			}
		}

		Vector3 randomPointWithLimits = model.transform.position - GetRandomPositionWithLimits (points, xAxis, yAxis, zAxis); 

		return randomPointWithLimits;
	}

	Vector3 GetRandomPositionWithLimits (List<Vector3> points, float[] xAxis, float[] yAxis, float[] zAxis, int iterationCount = 0) {
		Vector3 randomPoint = new Vector2 ();
		randomPoint.x = Random.Range (xAxis [0] + margin, xAxis [1] - margin);
		randomPoint.y = Random.Range (yAxis [0] + margin, yAxis [1] - margin);
		randomPoint.z = Random.Range (zAxis [0] + margin, zAxis [1] - margin);

		if (iterationCount > 100 || IsPointAvailable(randomPoint)) {
			return new Vector3 (randomPoint.x, randomPoint.y, randomPoint.z);
		} else {
			return GetRandomPositionWithLimits (points, xAxis, yAxis, zAxis, iterationCount + 1);
		}
	}

	bool IsPointAvailable (Vector3 position) {
		bool available = true;

		float[] xMargin = new float[] { position.x - margin, position.x + margin };
		float[] yMargin = new float[] { position.y - margin, position.y + margin };
		float[] zMargin = new float[] { position.z - margin, position.z + margin };

		foreach (Vector3 reservedPoint in reservedPositions) {
			if (reservedPoint.x > xMargin [0] && reservedPoint.x < xMargin [1]) {
				available = false;
			}

			if (reservedPoint.y > zMargin [0] && reservedPoint.y < zMargin [1]) {
				available = false;
			}
		}

		if (available) {
			reservedPositions.Add (position);
		}

		return available;
	}
}
