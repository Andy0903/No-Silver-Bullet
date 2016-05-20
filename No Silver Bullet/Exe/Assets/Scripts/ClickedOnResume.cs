using UnityEngine;
using System.Collections;

public class ClickedOnResume : MenuButtonClick
{
	#region Member variables
	public static bool myClickedOnResume;
	#endregion

	#region Public methods
	public override void OnClick ()
	{
		if (gameObject.tag == "ResumeButton")
		{
			myClickedOnResume = true;
		}
		else
		{
			myClickedOnResume = false;
		}
	}
	#endregion
}
