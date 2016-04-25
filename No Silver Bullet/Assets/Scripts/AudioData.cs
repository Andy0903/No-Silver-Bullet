using UnityEngine;
using System.Collections;

public class AudioData {

	public string myNameID;
	public AudioClip myAudioClip;

	public AudioData(string aFilePath, string aNameID)
	{
		
		myNameID = aNameID.Split ('.') [0]; //Ignores file ending
		myAudioClip = (AudioClip)Resources.Load (Application.dataPath+myNameID) as AudioClip;
	}

}
