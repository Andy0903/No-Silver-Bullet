using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class SoundManager : MonoBehaviour 
{
	public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
	public AudioSource myMusicSource;                 //Drag a reference to the audio source which will play the music.
	private List<AudioData> myBackgroundAudioData;  //All the data for the background music
	public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
	//public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
	//public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

	private const string BackgroundMusicFilePath = "Assets\\Sounds\\BackgroundMusic";
	private string myBackgroundMusicDirectory;
	private string[] myAudioFilePaths;
	private AudioClip myCurrentlyPlayingClip;

	void Awake ()
	{
		myBackgroundAudioData = new List<AudioData> ();

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

		myBackgroundMusicDirectory = Path.Combine(Directory.GetCurrentDirectory(), BackgroundMusicFilePath);

		myAudioFilePaths = Directory.GetFiles (myBackgroundMusicDirectory);
		foreach (string path in myAudioFilePaths)
		{
			string tempName = path.Split ('\\') [path.Split ('\\').Length - 1];

			if(path.Split ('.') [path.Split ('.').Length - 1] != "meta")
			{
				//Path.split is for getting a hold of the file name
				myBackgroundAudioData.Add(new AudioData(path, tempName));
			}

		}

		/*
		 //Used for debugging audiofiles in the AudioData list
		foreach(AudioData ad in myBackgroundAudioData)
		{
			print (ad.myNameID);
		}
		*/
	}

	//Might TODO: Remove parameter
	public void PlayBackgroundMusic(string aCurrentAct)
	{
		//TODO: Add functions for changing music during different acts
		efxSource.clip = myCurrentlyPlayingClip;

		efxSource.Play ();
	}

	public void StopBackgroundMusic()
	{
		efxSource.Stop ();
	}
		
}