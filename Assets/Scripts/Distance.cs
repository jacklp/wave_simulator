using System;
using UnityEngine;

//Help class to sort the distances
//Requires "using System;" at the top
public class Distance : IComparable<Distance> {
	//The distance to water
	public float distance;
	//We also need to store a name so we can form clockwise triangles
	public string name;
	//The Vector3 position of the vertice
	public Vector3 verticePos;

	public int CompareTo(Distance other) {
		return this.distance.CompareTo(other.distance);
	}
}