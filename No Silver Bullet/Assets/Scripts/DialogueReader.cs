using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

[XmlRoot("Dialogues")]
public static class DialogueReader
{

	#region Member variables

	//Default pathway for the dialogue file
	private const string FilePath = "Assets\\XML\\Dialogue.xml";

	[XmlArray("Characters")]
	[XmlArrayItem("Character")]
	public List<CharacterData> myCharacterDataList = new List<CharacterData> ();

	#endregion

	#region Constructors

	#endregion


	#region Public methods

	/// <summary>
	/// Loads the dialogue from the default path
	/// </summary>
	public static DialogueReader Load()
	{

		XmlSerializer serializer = new XmlSerializer(typeof(DialogueReader));

		FileStream fileStream = new FileStream(FilePath, FileMode.Open);

		DialogueReader dialogueReader = serializer.Deserialize (fileStream) as DialogueReader;

		return dialogueReader;
	}

	/// <summary>
	/// Load the dialogue from the specificed path
	/// </summary>
	/// <param name="aPath">A path.</param>
	public static DialogueReader Load(string aPath)
	{
		
		XmlSerializer serializer = new XmlSerializer(typeof(DialogueReader));

		FileStream fileStream = new FileStream(aPath, FileMode.Open);

		DialogueReader dialogueReader = serializer.Deserialize (fileStream) as DialogueReader;

		return dialogueReader;
	}

	/// <summary>
	/// Finds the characterr using the objects list
	/// </summary>
	/// <returns>The character. If character isn't found returns null</returns>
	/// <param name="aNameID">A name I.</param>
	public CharacterData FindCharacter(string aNameID)
	{
		foreach (CharacterData cd in myCharacterDataList)
		{
			if (cd.name == aNameID)
			{
				return cd;
			}
		}

		return null; 
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


