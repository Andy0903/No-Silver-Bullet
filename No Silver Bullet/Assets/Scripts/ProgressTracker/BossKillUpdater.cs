using UnityEngine;
using System.Collections;

public class BossKillUpdater : MonoBehaviour
{
	#region Member variables

	GameObject myPlayer;
	public int myActIndex;

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
			switch (myActIndex)
			{
			case 1:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct1Boss;
				break;
			case 2:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct2Boss;
				break;
			case 3:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct3Boss;
				break;
			case 4:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct4Boss;
				break;
			case 5:
				bossDefeatEnum = ProgressTracker.Quests.DefeatAct5Boss;
				break;
			default:
				throw new System.Exception ("Missing ActIndex");
			}

			myPlayer.GetComponent<PlayerController> ().myProgressTracker.SetQuestStatus (bossDefeatEnum, true);
		}
	}

	#endregion
}
