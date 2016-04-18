using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class CharacterData {

	[XmlAttribute("name")]
	public string name;

	[XmlElement("dialogue")]
	public string dialogue;
}
