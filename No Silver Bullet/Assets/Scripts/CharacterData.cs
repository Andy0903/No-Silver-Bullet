using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class CharacterData {

	[XmlAttribute("name")]
	public string name;


	[XmlArray("Dialogue")]
	[XmlArrayItem("Text")]
	public string[] dialogue;
}
