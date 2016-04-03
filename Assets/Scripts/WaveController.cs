using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class WaveController : MonoBehaviour {

	SineWaveModel sineWaveModel;

	void Start(){
		sineWaveModel = new SineWaveModel ();
		sineWaveModel.buildModel ();
	}
		
	public float GetWaveYPos(float x_coord, float z_coord) {
		float y_coord = 0f;



		if (sineWaveModel.modelData["continuousBool"] == 1f) {
			y_coord += sineWaveModel.modelData["amplitude"] * Mathf.Sin ((Time.time * sineWaveModel.modelData["speed"] + z_coord) / sineWaveModel.modelData["wavelength"]) + sineWaveModel.modelData["bias"];
		} else {
			// TODO: check time for how long left til it resets
			//sineWaveModel.modelData["timePeriod"]
		}

		if (sineWaveModel.modelData["noiseBool"] == 1f) {
			y_coord += Mathf.PerlinNoise (x_coord + sineWaveModel.modelData["noiseWalk"], z_coord + Mathf.Sin (Time.time * 0.1f)) * sineWaveModel.modelData["noiseStrength"];
		}

		return y_coord;
	}	
}
