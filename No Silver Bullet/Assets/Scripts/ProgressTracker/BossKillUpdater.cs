using UnityEngine;
using System.Collections;

public class BossKillUpdater : MonoBehaviour
{
	#region Member variables

	GameObject myPlayer;
	[SerializeField] int myActIndex;
	[SerializeField] int myItemDropIndex;

	#endregion

	#region Private methods

	private void Awake ()
	{
		myPlayer = GameObject.FindGameObjectWithTag ("Player");
	}

	private void Update ()
	{
		if (gameObject.GetComponent<EnemyHealth> ().CurrentHealth <= 0)
		{
			ProgressTracker.Quests bossDefeatEnum;
			GameObject[] tilesToBeRemoved = GameObject.FindGameObjectsWithTag ("QuestTile");

			switch (myActIndex)
			{
			case 1:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct1Boss;

				foreach (GameObject tile in tilesToBeRemoved)
				{
					Destroy (tile);
				}

				break;
			case 2:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct2Boss;

				foreach (GameObject tile in tilesToBeRemoved)
				{
					Destroy (tile);
				}

				break;
			case 3:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct3Boss;

				foreach (GameObject tile in tilesToBeRemoved)
				{
					Destroy (tile);
				}

				break;
			case 4:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct4Boss;

				foreach (GameObject tile in tilesToBeRemoved)
				{
					Destroy (tile);
				}

				break;
			case 5:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct5Boss;
				break;
			default:
				throw new System.Exception ("Missing ActIndex");
			}

			GameObject inventory = GameObject.FindGameObjectWithTag ("GUI").transform.FindChild ("InventoryGUI").gameObject;
			inventory.GetComponent<Inventory> ().AddItemToFirstEmptyCarryingSlot (myItemDropIndex);
			myPlayer.GetComponent<PlayerController> ().myProgressTracker.SetQuestStatus (bossDefeatEnum, true);

		}
	}

	#endregion
}
