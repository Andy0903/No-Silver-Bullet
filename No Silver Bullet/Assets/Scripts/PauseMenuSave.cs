using UnityEngine;
using System.Collections;

public class PauseMenuSave : MenuButtonClick
{
	#region Public methods

	public override void OnClick ()
	{
		SavedGame.SaveGame ();
	}

	#endregion
}
