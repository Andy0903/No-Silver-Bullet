using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
	#region Member variables

	[SerializeField] GameObject myPauseMenuManager;
	[SerializeField] GameObject myPlayerHealth;
	[SerializeField] GameObject myWinScreenManager;

	#endregion

	#region Public methods

	void Awake ()
	{
		
	}

	#endregion

	void Update ()
	{
		if (myPauseMenuManager.GetComponent<PauseMenumanager> ().IsPaused == true
		    || myPlayerHealth.GetComponent<PlayerHealth> ().IsAlive == false
		    || myWinScreenManager.GetComponent<WinScreenManager> ().HasWon == true)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
