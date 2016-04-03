using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class SineWaveModel {

	public Dictionary<string, float> modelData;

	public void buildModel () {

		modelData = new Dictionary<string, float> ();
		TextAsset asset = (TextAsset)Resources.Load("config", typeof(TextAsset));
		string element = "";

		if(asset != null)
		{
			XmlTextReader reader = new XmlTextReader(new StringReader(asset.text));
			while(reader.Read())
			{

				if (reader.NodeType == XmlNodeType.Element) {
					element = reader.GetAttribute("type");

				} else if (reader.NodeType == XmlNodeType.Text) {
					modelData.Add(element, float.Parse (reader.Value));
				}
			}
		}
	}
}
