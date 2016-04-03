﻿using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class WaveController : MonoBehaviour {

	SineWaveModel sineWaveModel;

	private Font f;
	GUIStyle backgroundStyle;
	GUIStyle labelStyle;
	GUIStyle sliderStyle;
	GUIStyle sliderThumbStyle;

	void Awake(){
		f = (Font)Resources.Load("tattoosailor");
		sineWaveModel = new SineWaveModel ();
		sineWaveModel.buildModel ();
	}

	void OnGUI()
	{
		backgroundStyle = new GUIStyle();
		//backgroundStyle.normal.background = MakeTex(600, 1, new Color(0f, 0f, 0f, 1.0f));

		labelStyle = new GUIStyle(GUI.skin.label);
		labelStyle.fontSize = 12;
		labelStyle.font = f;
		labelStyle.fixedWidth = 100;

		sliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
		sliderStyle.border.left = 2;
		sliderStyle.border.bottom = 2;
		sliderStyle.border.top = 2;
		sliderStyle.border.right = 2;

		sliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);


		//Begin Area
		GUILayout.BeginArea(new Rect(0,0, Screen.width/4, (Screen.height*2)/3), backgroundStyle);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Amplitude", labelStyle);
		float amplitude = GUILayout.HorizontalSlider ( sineWaveModel.modelData["amplitude"], 0.0F, 10.0F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Speed", labelStyle);
		float speed = GUILayout.HorizontalSlider ( sineWaveModel.modelData["speed"], 0.0F, 10.0F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Wavelength", labelStyle);
		float wavelength = GUILayout.HorizontalSlider ( sineWaveModel.modelData["wavelength"], 0.0F, 10.0F);
		GUILayout.EndHorizontal();
		GUILayout.Space(15);

		GUILayout.EndArea ();

		if(GUI.changed){
			sineWaveModel.modelData["amplitude"] = amplitude;
			sineWaveModel.modelData["speed"] = speed;
			sineWaveModel.modelData["wavelength"] = wavelength;
		}
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
