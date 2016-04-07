using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteWaves : MonoBehaviour {

	public Transform wave;
	GameObject Boat;
	List <Transform> waveGrid;
	public int numRows;

	void Start () {
		Boat = GameObject.Find("Boat");

		//instantiate a grid of 9 waves.
		for (int z = 0; z < numRows; z++) {
			for (int x = 0; x < numRows; x++) {
				Transform ic = Instantiate (wave, new Vector3(x*30, 0, z*30), Quaternion.identity) as Transform;
				ic.parent=transform;
				waveGrid.Add (ic);
			}
		}
		transform.Rotate (new Vector3 (0, 45, 0));
	}

	void Update () {

		//TODO: if player position close to the edge of one of the waves then draw more of them.

		//Create Grid of Waves and store them in List.	
		for (int z = 0; z < 5; z++) {
			for (int x = 0; x < 5; x++) {
				
			}
		}
	


	}
}
