using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

[XmlRoot("Dialogues")]
public class DialogueReader
{

	#region Member variables

	//XmlReader myReader;



	[XmlArray("npcs")]
	[XmlArrayItem("npc")]
	public List<Dialogue> myDialogue = new List<Dialogue> ();
	#endregion


	#region Constructors

	#endregion


	#region Public methods

	public static DialogueReader Load(string aPath)
	{
		
		// Construct an instance of the XmlSerializer with the type
		// of object that is being deserialized.
		XmlSerializer serializer = new XmlSerializer(typeof(DialogueReader));
		// To read the file, create a FileStream.
		FileStream fileStream = new FileStream(aPath, FileMode.Open);
		// Call the Deserialize method and cast to the object type.
		DialogueReader dialogueReader = serializer.Deserialize (fileStream) as DialogueReader;

		return dialogueReader;



		/*
		TextAsset xml = Resources.Load<TextAsset> (aPath);

		XmlSerializer serializer = new XmlSerializer (typeof(DialogueReader));
		StringReader reader = new StringReader(xml.text);

		DialogueReader dialogueReader = serializer.Deserialize (reader) as DialogueReader;

		reader.Close ();

		return dialogueReader; 
		*/
	}

	#endregion

	#region Private methods

	#endregion

}
