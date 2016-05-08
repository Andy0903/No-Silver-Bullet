using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
	#region Member variables

	[SerializeField] PauseMenuManager myPauseMenumanager;
	[SerializeField] PlayerHealth myPlayerHealth;

	#endregion

	#region Public methods

	void Awake ()
	{
		
	}

	#endregion

	void Update ()
	{
		if (myPauseMenumanager.IsPaused == true || myPlayerHealth.IsAlive == false)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
