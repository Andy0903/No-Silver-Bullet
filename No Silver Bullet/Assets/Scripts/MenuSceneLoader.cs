using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuSceneLoader : MenuButtonClick
{
	#region Member variables

	public string myLeadsToSceneName;
	public AudioClip myActClip;

	#endregion

	#region Public methods

	public override void OnClick ()
	{
		SceneManager.LoadScene ("Scenes/" + myLeadsToSceneName);

		if (myActClip != null)
		{
			SoundManager.instance.ChangeBGMusic (myActClip);
		}
	}

	#endregion

}
