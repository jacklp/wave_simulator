using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEditor;

public class WaveController : MonoBehaviour {

	SineWaveModel sineWaveModel;
	public GUISkin menuSkin;
	public List<Vector3> direction;

	void Awake(){
		sineWaveModel = new SineWaveModel ();
		sineWaveModel.buildModel ();
		direction = new List<Vector3> ();
		direction.Add (new Vector3(2,5,1));
	}

	void OnGUI()
	{
		GUI.skin = menuSkin;

		EditorStyles.radioButton.font = menuSkin.label.font;
		EditorStyles.radioButton.normal.textColor = menuSkin.label.normal.textColor;
		EditorStyles.radioButton.onNormal.textColor = menuSkin.label.onNormal.textColor;


		//Begin Area
		GUILayout.BeginArea (new Rect (0, 0, Screen.width / 4, 600), menuSkin.box);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Amplitude");
		float amplitude = GUILayout.HorizontalSlider (sineWaveModel.modelData ["amplitude"], 0.0F, 2.5F);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Speed");
		float speed = GUILayout.HorizontalSlider (sineWaveModel.modelData ["speed"], 0.0F, 2F);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Wavelength");
		float wavelength = GUILayout.HorizontalSlider (sineWaveModel.modelData ["wavelength"], 5F, 10F);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		int noiseBoolVal = (int)sineWaveModel.modelData ["noiseBool"];
		int noiseBool = GUILayout.SelectionGrid (noiseBoolVal, new string[] { "Noise Off", "Noise On" }, 1, EditorStyles.radioButton);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Noise Strength");
		float noiseStrength = GUILayout.HorizontalSlider (sineWaveModel.modelData ["noiseStrength"], 0.0F, 6.0F);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Noise Walk");
		float noiseWalk = GUILayout.HorizontalSlider (sineWaveModel.modelData ["noiseWalk"], 0.0F, 2.0F);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Bias");
		float bias = GUILayout.HorizontalSlider (sineWaveModel.modelData ["bias"], 0.0F, 2.0F);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		int continuousBoolVal = (int)sineWaveModel.modelData ["continuousBool"];
		int continuousBool = GUILayout.SelectionGrid (continuousBoolVal, new string[] { "Time Period", "Continuous" }, 1, EditorStyles.radioButton);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Time Period");
		float timePeriod = GUILayout.HorizontalSlider (sineWaveModel.modelData ["timePeriod"], 0.1F, 2F);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label("Steepness");
		int steepness = GUILayout.SelectionGrid ((int)sineWaveModel.modelData ["steepness"], new string[] { "off", "1", "2", }, 1, EditorStyles.radioButton);
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label("Direction x");
		string directionx = GUILayout.TextField (sineWaveModel.modelData ["directionx"].ToString());
		if (directionx == "") {
			directionx = "0";
		}
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);

		GUILayout.BeginHorizontal ();
		GUILayout.Label("Direction y");
		string directiony = GUILayout.TextField (sineWaveModel.modelData ["directiony"].ToString());
		GUILayout.EndHorizontal ();
		GUILayout.Space (15);
		if (directiony == "") {
			directiony = "0";
		}


		GUILayout.EndArea ();

		if (GUI.changed) {
			sineWaveModel.modelData ["amplitude"] = amplitude;
			sineWaveModel.modelData ["speed"] = speed;
			sineWaveModel.modelData ["wavelength"] = wavelength;
			sineWaveModel.modelData ["noiseBool"] = (float)noiseBool;
			sineWaveModel.modelData ["noiseStrength"] = noiseStrength;
			sineWaveModel.modelData ["noiseWalk"] = noiseWalk;
			sineWaveModel.modelData ["bias"] = bias;
			sineWaveModel.modelData ["continuousBool"] = (float)continuousBool;
			sineWaveModel.modelData ["timePeriod"] = timePeriod;
			sineWaveModel.modelData ["steepness"] = (float)steepness;
			sineWaveModel.modelData ["directionx"] = float.Parse (directionx);
			sineWaveModel.modelData ["directiony"] = float.Parse (directiony);
			sineWaveModel.saveModel ();
		}
	}
		
	public float GetWaveYPos(float x_coord, float z_coord, float index) {
		float y_coord = 0f;

		if (sineWaveModel.modelData["continuousBool"] == 1f) {
			y_coord += sineWaveModel.modelData["amplitude"] * Mathf.Pow(Mathf.Sin ( (2* Mathf.PI * Time.time * sineWaveModel.modelData["speed"] + Vector2.Dot (new Vector2(sineWaveModel.modelData ["directionx"], sineWaveModel.modelData ["directiony"]), new Vector2 (x_coord, z_coord))) / sineWaveModel.modelData["wavelength"]) + sineWaveModel.modelData["bias"], sineWaveModel.modelData ["steepness"]);
		} else {
			y_coord += sineWaveModel.modelData["amplitude"] * Mathf.Sin ( (2* Mathf.PI * 1/(sineWaveModel.modelData ["timePeriod"] * sineWaveModel.modelData["speed"]) + z_coord) / sineWaveModel.modelData["wavelength"]) + sineWaveModel.modelData["bias"];
		}

		if (sineWaveModel.modelData["noiseBool"] == 1f) {
			y_coord += Mathf.PerlinNoise (x_coord + sineWaveModel.modelData["noiseWalk"] + this.transform.position.x, 
				z_coord + Mathf.Sin (Time.time * 0.1f) + this.transform.position.z)
				* sineWaveModel.modelData["noiseStrength"];
		}

		return y_coord;
	}	

	private Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width * height];

		for (int i = 0; i < pix.Length; i++)
			pix[i] = col;

		Texture2D result = new Texture2D(width, height);
		result.SetPixels(pix);
		result.Apply();

		return result;
	}
}
