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

	void Awake(){
		sineWaveModel = new SineWaveModel ();
		sineWaveModel.buildModel ();
	}
		
	void OnGUI()
	{
		GUI.skin = menuSkin;

		EditorStyles.radioButton.font = menuSkin.label.font;
		EditorStyles.radioButton.normal.textColor = menuSkin.label.normal.textColor;
		EditorStyles.radioButton.onNormal.textColor = menuSkin.label.onNormal.textColor;


		//Begin Area
		GUILayout.BeginArea(new Rect(0,0, Screen.width/4, 400), menuSkin.box);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Amplitude");
		float amplitude = GUILayout.HorizontalSlider ( sineWaveModel.modelData["amplitude"], 0.0F, 5F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Speed");
		float speed = GUILayout.HorizontalSlider ( sineWaveModel.modelData["speed"], 0.0F, 5F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Wavelength");
		float wavelength = GUILayout.HorizontalSlider ( sineWaveModel.modelData["wavelength"], 0.0F, 10F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		int noiseBoolVal = (int)sineWaveModel.modelData ["noiseBool"];
		int noiseBool = GUILayout.SelectionGrid(noiseBoolVal, new string[] { "Noise Off", "Noise On"}, 1, EditorStyles.radioButton);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Noise Strength");
		float noiseStrength = GUILayout.HorizontalSlider ( sineWaveModel.modelData["noiseStrength"], 0.0F, 10.0F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Noise Walk");
		float noiseWalk = GUILayout.HorizontalSlider ( sineWaveModel.modelData["noiseWalk"], 0.0F, 10.0F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Bias");
		float bias = GUILayout.HorizontalSlider ( sineWaveModel.modelData["bias"], 0.0F, 10.0F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		int continuousBoolVal = (int)sineWaveModel.modelData ["continuousBool"];
		int continuousBool = GUILayout.SelectionGrid(continuousBoolVal, new string[] { "Time Period", "Continuous"}, 1, EditorStyles.radioButton);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Time Period");
		float timePeriod = GUILayout.HorizontalSlider ( sineWaveModel.modelData["timePeriod"], 0.2F, 1F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.EndArea ();

		if(GUI.changed){
			sineWaveModel.modelData["amplitude"] = amplitude;
			sineWaveModel.modelData["speed"] = speed;
			sineWaveModel.modelData["wavelength"] = wavelength;
			sineWaveModel.modelData ["noiseBool"] = (float)noiseBool;
			sineWaveModel.modelData ["noiseStrength"] = noiseStrength;
			sineWaveModel.modelData ["noiseWalk"] = noiseWalk;
			sineWaveModel.modelData ["bias"] = bias;
			sineWaveModel.modelData ["continuousBool"] = (float)continuousBool;
			sineWaveModel.modelData ["timePeriod"] = timePeriod;
			sineWaveModel.saveModel ();
		}
	}
		
	public float GetWaveYPos(float x_coord, float z_coord) {
		float y_coord = 0f;

		if (sineWaveModel.modelData["continuousBool"] == 1f) {
			y_coord += sineWaveModel.modelData["amplitude"] * Mathf.Sin ( (2* Mathf.PI * Time.time * sineWaveModel.modelData["speed"] + z_coord) / sineWaveModel.modelData["wavelength"]) + sineWaveModel.modelData["bias"];
		} else {
			y_coord += sineWaveModel.modelData["amplitude"] * Mathf.Sin ( (2* Mathf.PI * 1/(sineWaveModel.modelData ["timePeriod"] * sineWaveModel.modelData["speed"]) + z_coord) / sineWaveModel.modelData["wavelength"]) + sineWaveModel.modelData["bias"];
		}

		if (sineWaveModel.modelData["noiseBool"] == 1f) {
			y_coord += Mathf.PerlinNoise (x_coord + sineWaveModel.modelData["noiseWalk"], z_coord + Mathf.Sin (Time.time * 0.1f)) * sineWaveModel.modelData["noiseStrength"];
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
