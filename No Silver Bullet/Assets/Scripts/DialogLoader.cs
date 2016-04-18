using UnityEngine;
using System.Collections;

public class DialogLoader : MonoBehaviour {


	public const string FilePath = "Assets\\XML\\Dialogue.xml";


	// Use this for initialization
	void Start () {
		
		DialogueReader dialogues = DialogueReader.Load (FilePath);

		foreach (Dialogue dialog in dialogues.myDialogue)
		{
			print (dialog.dialogue);
		}

	}

}
