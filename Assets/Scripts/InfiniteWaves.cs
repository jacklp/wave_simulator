﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Tile{
	public GameObject theTile;
	public float creationTime;

	public Tile(GameObject t, float ct){
		theTile = t;
		creationTime = ct;
	}
}

public class InfiniteWaves : MonoBehaviour {

	public GameObject plane;
	public GameObject player;

	public int planeSize;
	public int halfTilesX;
	public int halfTilesZ;

	Vector3 startPos;

	Hashtable tiles = new Hashtable();

	Material wireframe;
	Material water;
	bool wireOn;


	void Start(){

		wireOn = false;

		water = Resources.Load ("water") as Material;
		wireframe = Resources.Load ("wireframev2") as Material;

		this.gameObject.transform.position = Vector3.zero;
		startPos = Vector3.zero;

		float updateTime = Time.realtimeSinceStartup;

		for (int x = -halfTilesX; x < halfTilesX; x++) {
			for (int z = -halfTilesZ; z < halfTilesZ; z++) {
				Vector3 pos = new Vector3(	(x * planeSize + startPos.x),
											0,
											(z*planeSize + startPos.z)
										);
				GameObject t = (GameObject)Instantiate (plane, pos, Quaternion.identity);
				string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
				t.name = tilename;
				t.tag = "plane";
				Tile Tile = new Tile(t, updateTime);
				tiles.Add(tilename, Tile);
			}
		}



	}
	
	void Update(){

		if (Input.GetKeyDown ("space")) {

			if (wireOn) {

				plane.GetComponent<Renderer> ().material = wireframe;

				foreach(DictionaryEntry entry in tiles){
					Tile tile = entry.Value as Tile;
					tile.theTile.GetComponent<Renderer> ().material = wireframe;
				}
					
				wireOn = false;
			} else {
				plane.GetComponent<Renderer> ().material = water;
				foreach(DictionaryEntry entry in tiles){
					Tile tile = entry.Value as Tile;
					tile.theTile.GetComponent<Renderer> ().material = water;
				}
				wireOn = true;
			}
		}


		int xMove = (int)(player.transform.position.x - startPos.x);
		int zMove = (int)(player.transform.position.z - startPos.z);

		if (Mathf.Abs (xMove) >= planeSize || Mathf.Abs (zMove) >= planeSize) {
			float updateTime = Time.realtimeSinceStartup;

			int playerX = (int)(Mathf.Floor (player.transform.position.x / planeSize) * planeSize);
			int playerZ = (int)(Mathf.Floor (player.transform.position.z / planeSize) * planeSize);

			for (int x = -halfTilesX; x < halfTilesX; x++) {
				for (int z = -halfTilesZ; z < halfTilesZ; z++) {
					Vector3 pos = new Vector3(	(x * planeSize + playerX),
						0,
						(z*planeSize + playerZ)
					);

					string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
					if (!tiles.ContainsKey (tilename)) {
						GameObject t = (GameObject)Instantiate (plane, pos, Quaternion.identity);
						t.name = tilename;
						Tile Tile = new Tile (t, updateTime);
						tiles.Add (tilename, Tile);
					} else {
						(tiles [tilename] as Tile).creationTime = updateTime;
					}
						
				}
			}

			Hashtable newTerrain = new Hashtable ();
			foreach (Tile tls in tiles.Values) {
				if (tls.creationTime != updateTime) {
					Destroy (tls.theTile);
				} else {
					newTerrain.Add (tls.theTile.name, tls);
				}
			}
			tiles = newTerrain;

			startPos = player.transform.position;
		}
	}
}
