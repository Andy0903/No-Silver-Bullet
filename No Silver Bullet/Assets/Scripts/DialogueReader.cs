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

	[XmlArray("Characters")]
	[XmlArrayItem("Character")]
	public List<CharacterData> myCharacterDataList = new List<CharacterData> ();

	#endregion

	#region Constructors

	#endregion


	#region Public methods

	public static DialogueReader Load(string aPath)
	{
		
		XmlSerializer serializer = new XmlSerializer(typeof(DialogueReader));

		FileStream fileStream = new FileStream(aPath, FileMode.Open);

		DialogueReader dialogueReader = serializer.Deserialize (fileStream) as DialogueReader;

		return dialogueReader;
	}

	/// <summary>
	/// Finds the character.
	/// </summary>
	/// <returns>The character. If not found returns null</returns>
	/// <param name="aCharacterDataList">A character data list.</param>
	/// <param name="aNameID">Search ID of the character</param>
	public static CharacterData FindCharacter(List<CharacterData> aCharacterDataList, string aNameID)
	{
		foreach (CharacterData cd in aCharacterDataList)
		{
			if (cd.name == aNameID)
			{
				return cd;
			}
		}

		return null; 
	}

	#endregion

	#region Private methods

	#endregion

}


