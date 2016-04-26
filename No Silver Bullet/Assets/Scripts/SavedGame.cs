﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class SavedGame
{
	#region Member variables

	const string FilePath = "Assets\\XML\\SaveGame.xml";
	public float myPlayerX;
	public float myPlayerY;
	public ProgressTracker myProgressTracker;
	public string myCurrentScene;
	public string myMusicSourceClipName;

	#endregion

	#region Private methods

	public static void SaveGame ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		SavedGame save = new SavedGame ();
		save.myPlayerX = player.transform.position.x;
		save.myPlayerY = player.transform.position.y;
		save.myProgressTracker = player.GetComponent<PlayerController> ().ProgressTracker;
		save.myCurrentScene = SceneManager.GetActiveScene ().name;
		save.myMusicSourceClipName = SoundManager.instance.myMusicSource.clip.name; 
		//TODO inventory

		XmlSerializer serializer = new XmlSerializer (typeof(SavedGame));
		using (StreamWriter writer = new StreamWriter (FilePath))
		{
			serializer.Serialize (writer, save);
		}
	}

	public static SavedGame LoadGame ()
	{
		SavedGame savedGame;

		XmlSerializer serializer = new XmlSerializer (typeof(SavedGame));
		using (FileStream filestream = new FileStream (FilePath, FileMode.Open))
		{
			savedGame = (SavedGame)serializer.Deserialize (filestream);
		}

		return savedGame;
	}

	
	#endregion
}