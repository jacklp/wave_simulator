using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;

public class WaveController : MonoBehaviour {

	public float scale = 0.1f;
	public float speed = 1.0f;
	//The width between the wave peaks
	public float waveDistance = 0.2f;
	//Noise parameters
	public float noiseStrength = 1f;
	public float noiseWalk = 1f;

	/*void Start(){
		
		File configs = Resources.Load
		using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
		{
			XmlWriterSettings ws = new XmlWriterSettings();
			ws.Indent = true;
			using (XmlWriter writer = XmlWriter.Create(output, ws))
			{

				// Parse the file and display each of the nodes.
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
					case XmlNodeType.Element:
						writer.WriteStartElement(reader.Name);
						break;
					case XmlNodeType.Text:
						writer.WriteString(reader.Value);
						break;
					case XmlNodeType.XmlDeclaration:
					case XmlNodeType.ProcessingInstruction:
						writer.WriteProcessingInstruction(reader.Name, reader.Value);
						break;
					case XmlNodeType.Comment:
						writer.WriteComment(reader.Value);
						break;
					case XmlNodeType.EndElement:
						writer.WriteFullEndElement();
						break;
					}
				}

			}
		}
		OutputTextBlock.Text = output.ToString();
	}*/

	public float GetWaveYPos(float x_coord, float z_coord) {
		float y_coord = 0f;
	
		y_coord += Mathf.Sin((Time.time * speed + z_coord) / waveDistance) * scale;

		//add noise to make it more realistic
		y_coord += Mathf.PerlinNoise(x_coord + noiseWalk, z_coord + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;

		return y_coord;
	}	
}
