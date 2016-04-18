using UnityEngine;
using System.Collections;

public class AudioData {

	public string myNameID;
	public AudioClip myAudioClip;

	public AudioData(string aFilePath, string aNameID)
	{
		myAudioClip = Resources.Load (aFilePath) as AudioClip;
		myNameID = aNameID;
	}

}
