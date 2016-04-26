using UnityEngine;
using System.Collections;

public class QuitApplication : MenuButtonClick
{
	#region Public methods

	public override void OnClick ()
	{
		Application.Quit ();
	}

	#endregion
}
