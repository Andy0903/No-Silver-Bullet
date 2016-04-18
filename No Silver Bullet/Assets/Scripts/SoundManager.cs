using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class SoundManager : MonoBehaviour 
{
	//public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource myMusicSource;                 //Drag a reference to the audio source which will play the music.
	private List<AudioData> myBackgroundAudioData;  //All the data for the background music
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
	public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

	private string BackgroundMusicDirectory = Path.Combine(Directory.GetCurrentDirectory(), "\\Assets\\Sounds\\BackgroundMusic");
	private string[] myAudioFilePaths;


	void Awake ()
	{
		//Check if there is already an instance of SoundManager
		if (instance == null)
			//if not, set it to this.
			instance = this;
		//If instance already exists:
		else if (instance != this)
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);

		myAudioFilePaths = Directory.GetFiles (BackgroundMusicDirectory);
		foreach (string path in myAudioFilePaths)
		{
			//Path.split is for getting a hold of the file name
			myBackgroundAudioData.Add(new AudioData(path, path.Split('\\')[path.Split('\\').Length-1]));
		}

		
	}
		
}