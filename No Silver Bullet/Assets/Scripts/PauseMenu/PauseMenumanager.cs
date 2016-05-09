using UnityEngine;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
	#region Member variables

	public GameObject myCanvas;
	[SerializeField] GameObject myPlayer;

	#endregion

	#region Properties

	public bool IsPaused
	{
		get;
		private set;
	}

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
		IsPaused = !IsPaused;
	}

	private void Start ()
	{
		IsPaused = false;
		myCanvas.SetActive (IsPaused);
	}

	private void Update ()
	{
		ReadInput ();
		PausedUpdates ();
	}

	private void ReadInput ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && myPlayer.GetComponent<PlayerHealth> ().IsAlive == true)
		{
			ShiftPausedState ();
		}
	}

	private void PausedUpdates ()
	{
		myCanvas.SetActive (IsPaused);
	}

	#endregion

}
