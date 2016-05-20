using UnityEngine;
using System.Collections;

public class PauseMenuResume : MenuButtonClick
{
	#region Events

	public delegate void ClickAction ();

	public static event ClickAction OnClicked;

	#endregion

	#region Public methods

	public override void OnClick ()
	{
		OnClicked ();
	}

	#endregion
}
