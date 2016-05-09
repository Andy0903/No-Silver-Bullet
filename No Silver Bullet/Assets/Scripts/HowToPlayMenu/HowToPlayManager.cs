using UnityEngine;
using System.Collections;

public class HowToPlayManager : MonoBehaviour
{
	#region Member variables

	public GameObject myCanvas;

	#endregion

	#region Properties

	public bool InHowToPlay
	{
		get;
		private set;
	}

	#endregion

	#region Private methods

	void OnEnable ()
	{
		HowToPlayButton.OnClicked += ShiftHowToPlayState;
	}


	void OnDisable ()
	{
		HowToPlayButton.OnClicked -= ShiftHowToPlayState;
	}

	private void ShiftHowToPlayState ()
	{
		InHowToPlay = !InHowToPlay;
	}

	private void Start ()
	{
		InHowToPlay = false;
		myCanvas.SetActive (InHowToPlay);
	}

	private void Update ()
	{
		myCanvas.SetActive (InHowToPlay);
	}

	#endregion

}
