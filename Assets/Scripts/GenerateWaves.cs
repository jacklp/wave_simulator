using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateWaves : MonoBehaviour {

	//The water mesh
	Mesh waterMesh;

	//The new values of the vertices after we applied the wave algorithm are stored here
	private Vector3[] newVertices;
	//The initial values of the vertices are stored here
	private Vector3[] originalVertices;

	private Vector3[] waveVertices;
	//To get the y position
	private WaveController waveScript;

	public List<Vector3> waves;



	void Start() {

		waves = new List<Vector3> ();
		waterMesh = this.GetComponent<MeshFilter> ().mesh;
		originalVertices = waterMesh.vertices;
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");
		waveScript = gameController.GetComponent<WaveController> ();
			
	}


	void Update() {

		MoveSea ();

	}
		

	void MoveSea() {

			
		newVertices = new Vector3[originalVertices.Length];

		for (int i = 0; i < originalVertices.Length; i++) {
			Vector3 vertice = originalVertices[i];

			vertice = transform.TransformPoint(vertice);
			vertice.y += waveScript.GetWaveYPos(vertice.x, vertice.z, i);
			newVertices[i] = transform.InverseTransformPoint(vertice);
		}

		//Add the new position of the water to the water mesh
		waterMesh.vertices = newVertices;
		//After modifying the vertices it is often useful to update the normals to reflect the change
		waterMesh.RecalculateNormals();
	}
		
}
