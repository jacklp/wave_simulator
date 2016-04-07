using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class SineWaveModel {

	public Dictionary<string, float> modelData;
	TextAsset asset;

	public void buildModel () {

		modelData = new Dictionary<string, float> ();
		asset = (TextAsset)Resources.Load("config", typeof(TextAsset));
		string element = "";

		if(asset != null)
		{
			XmlTextReader reader = new XmlTextReader(new StringReader(asset.text));
			while(reader.Read())
			{

				if (reader.NodeType == XmlNodeType.Element) {
					element = reader.Name;

				} else if (reader.NodeType == XmlNodeType.Text) {
					modelData.Add(element, float.Parse (reader.Value));
				}
			}
		}
	}

	public void saveModel(){
	  
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml ( asset.text );
		XmlNode root = doc.DocumentElement;


		foreach(KeyValuePair<string, float> entry in modelData)
		{
			//XmlTextWriter writer = new XmlTextWriter(new XmlTextWriter());
			XmlNode myNode = root.SelectSingleNode(entry.Key);
			myNode.InnerText = entry.Value.ToString();
			doc.Save(Application.dataPath.ToString() + "/Resources/config.xml");
		}
	}
}
