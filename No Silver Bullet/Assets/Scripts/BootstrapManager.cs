using UnityEngine;
using System.Collections;

public class BootstrapManager : MonoBehaviour
{
	#region Member variables

	public GameObject Act1Changer;
	public GameObject Act2Changer;
	public GameObject Act3Changer;
	public GameObject Act4Changer;
	public GameObject Act5Changer;

	#endregion

	#region Private methods

	private void Awake ()
	{
		GameObject toInstantiate;
		if (ClickedOnResume.myClickedOnResume == true)
		{
			SavedGame lastSavedGame = SavedGame.LoadGame ();
			GameObject inventory = GameObject.FindGameObjectWithTag ("GUI").transform.FindChild ("InventoryGUI").gameObject;
			inventory.GetComponent<Inventory> ().LoadInventory (lastSavedGame.myInventoryInformation);

			switch (lastSavedGame.myCurrentScene)
			{
			case "Scenes/Scene":
				toInstantiate = Act1Changer;
				break;
			case "Scenes/Act2":
				toInstantiate = Act2Changer;
				break;
			case "Scenes/Act3":
				toInstantiate = Act3Changer;
				break;
			case "Scenes/Act4":
				toInstantiate = Act4Changer;
				break;
			case "Scenes/Act5":
				toInstantiate = Act5Changer;
				break;
			default:
				throw new System.Exception ("Faulty Scene in load");
			}

			GameObject instance = Instantiate (toInstantiate, Vector2.zero, Quaternion.identity) as GameObject;
			GameObject player = GameObject.FindGameObjectWithTag ("Player");

			player.GetComponent<PlayerController> ().myProgressTracker = lastSavedGame.myProgressTracker;
			inventory.GetComponent<Inventory> ().HasChanged ();

		}
		else
		{
			toInstantiate = Act5Changer;//Act1Changer;
			GameObject instance = Instantiate (toInstantiate, Vector2.zero, Quaternion.identity) as GameObject;
		}
	}

	#endregion
}
