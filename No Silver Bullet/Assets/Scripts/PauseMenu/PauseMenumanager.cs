using UnityEngine;
using System.Collections;

public class PauseMenumanager : MonoBehaviour
{
	#region Member variables

	bool myIsPaused;
	public GameObject myCanvas;

	#endregion

	#region Private methods

	void OnEnable ()
	{
		PauseMenuResume.OnClicked += ShiftPausedState;
	}


	void OnDisable ()
	{
		PauseMenuResume.OnClicked -= ShiftPausedState;
	}

	private void ShiftPausedState ()
	{
		myIsPaused = !myIsPaused;
	}

	private void Start ()
	{
		myIsPaused = false;
		myCanvas.SetActive (myIsPaused);
	}

	private void Update ()
	{
		ReadInput ();
		PausedUpdates ();
	}

	private void ReadInput ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			ShiftPausedState ();
		}
	}

	private void PausedUpdates ()
	{
		if (myIsPaused == true)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}

		myCanvas.SetActive (myIsPaused);
	}

	#endregion

}
