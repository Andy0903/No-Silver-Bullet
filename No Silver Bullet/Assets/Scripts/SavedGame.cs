using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SavedGame
{
	#region Member variables

	const string FilePath = "Assets\\XML\\SaveGame.xml";
	public float myPlayerX;
	public float myPlayerY;
	public ProgressTracker myProgressTracker;
	public string myCurrentScene;
	public SerializableDictionary<int, int> myInventoryInformation;

	#endregion

	#region Private methods

	public static void SaveGame ()
	{
		ClickedOnResume.myClickedOnResume = true;

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject inventory = GameObject.FindGameObjectWithTag ("GUI").transform.FindChild ("InventoryGUI").gameObject;
		SavedGame save = new SavedGame ();
		save.myPlayerX = player.transform.position.x;
		save.myPlayerY = player.transform.position.y;
		save.myProgressTracker = player.GetComponent<PlayerController> ().myProgressTracker;
		save.myCurrentScene = SceneManager.GetActiveScene ().name;
		save.myInventoryInformation = inventory.GetComponent<Inventory> ().InventoryInformation ();

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