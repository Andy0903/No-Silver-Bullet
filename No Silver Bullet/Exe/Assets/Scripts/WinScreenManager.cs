using UnityEngine;
using System.Collections;

public class WinScreenManager : MonoBehaviour
{
	#region Member variables

	public GameObject myCanvas;
	[SerializeField] GameObject myPlayer;

	#endregion

	#region Properties

	public bool HasWon
	{
		get;
		private set;
	}

	#endregion

	#region Private methods

	private void Start ()
	{
		HasWon = false;
		myCanvas.SetActive (HasWon);
	}

	private void Update ()
	{
		if (myPlayer.GetComponent<PlayerController> ().myProgressTracker.GetQuestStatus (ProgressTracker.Quests.DefeatAct5Boss) == true)
		{
			HasWon = true;
			myCanvas.SetActive (HasWon);
		}
	}

	#endregion

}
