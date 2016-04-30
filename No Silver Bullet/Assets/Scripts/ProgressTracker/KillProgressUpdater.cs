using UnityEngine;
using System.Collections;

public class KillProgressUpdater : MonoBehaviour
{
	#region Member variables

	GameObject myPlayer;

	#endregion

	#region Private methods

	void Awake ()
	{
		myPlayer = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update ()
	{
		if (gameObject.GetComponent<EnemyHealth> ().CurrentHealth <= 0)
		{
			int aliveEnemiesForQuestFoundBeforeRemovingThis = 0;

			if (myPlayer.GetComponent<PlayerController> ().myProgressTracker.GetQuestStatus (gameObject.GetComponent<QuestLinker> ().myBelongsToQuest) != true)
			{
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

				foreach (GameObject enemy in enemies)
				{
					if (enemy.GetComponent<QuestLinker> () != null && enemy.GetComponent<QuestLinker> ().myBelongsToQuest == gameObject.GetComponent<QuestLinker> ().myBelongsToQuest)
					{
						aliveEnemiesForQuestFoundBeforeRemovingThis++;
					}
				}

				Debug.Log (aliveEnemiesForQuestFoundBeforeRemovingThis);
				if (aliveEnemiesForQuestFoundBeforeRemovingThis == 1)
				{
					myPlayer.GetComponent<PlayerController> ().myProgressTracker.SetQuestStatus (gameObject.GetComponent<QuestLinker> ().myBelongsToQuest, true);
				}

			}
		}
	}

	#endregion

}
