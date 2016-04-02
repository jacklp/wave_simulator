using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {

	public float scale = 0.1f;
	public float speed = 1.0f;
	//The width between the wave peaks
	public float waveDistance = 1f;
	//Noise parameters
	public float noiseStrength = 1f;
	public float noiseWalk = 1f;

	public float GetWaveYPos(float x_coord, float z_coord) {
		float y_coord = 0f;
	
		y_coord += Mathf.Sin((Time.time * speed + z_coord) / waveDistance) * scale;

		//add noise to make it more realistic
		y_coord += Mathf.PerlinNoise(x_coord + noiseWalk, z_coord + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;

		return y_coord;
	}	
}
