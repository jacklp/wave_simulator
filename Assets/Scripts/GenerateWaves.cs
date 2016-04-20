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

	int numOfWaves = 1;
	public List<Vector3> waves;

	Material wireframe;
	Material water;
	bool wireOn;

	void Start() {

		waves = new List<Vector3> ();

		wireOn = false;
		water = Resources.Load ("water") as Material;
		wireframe = Resources.Load ("wireframev2") as Material;

		//Get the water mesh
		waterMesh = this.GetComponent<MeshFilter> ().mesh;

		originalVertices = waterMesh.vertices;

		//Get the waveScript
		GameObject gameController = GameObject.FindGameObjectWithTag ("GameController");

		waveScript = gameController.GetComponent<WaveController> ();
			
	}


	void Update() {


		MoveSea ();

		if (Input.GetKeyDown ("space")) {

			if (wireOn) {
				GetComponent<Renderer> ().material = wireframe;	
				wireOn = false;
			} else {
				GetComponent<Renderer> ().material = water;
				wireOn = true;
			}
		}
	}
		

	void MoveSea() {

			
		newVertices = new Vector3[originalVertices.Length];


		for (int i = 0; i < originalVertices.Length; i++) {
			Vector3 vertice = originalVertices[i];

			//Now we need to modify this coordinate's y-position
			//From local to global
			vertice = transform.TransformPoint(vertice);


			vertice.y += waveScript.GetWaveYPos(vertice.x, vertice.z, i);


			//vertice = waveScript.GerstnerWaveFunction (vertice.x, vertice.y, vertice.z, waves);
			//vertice.y = waveScript.sumSinWaves(vertice.x, vertice.y, vertice.z, waves);

			//From global to local
			newVertices[i] = transform.InverseTransformPoint(vertice);
		}

		//Add the new position of the water to the water mesh
		waterMesh.vertices = newVertices;
		//After modifying the vertices it is often useful to update the normals to reflect the change
		waterMesh.RecalculateNormals();
	}
		
}
