using UnityEngine;
using System.Collections;
using System.IO;

public class DialogLoader : MonoBehaviour {


	public const string FilePath = "Assets\\XML\\Dialogue.xml";


	// Use this for initialization
	void Start () {
		
		DialogueReader dialogues = DialogueReader.Load (FilePath);

		foreach (CharacterData cd in dialogues.myCharacterDataList)
		{
			print (cd.dialogue);
		}

	}

}
