using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButton : MenuButtonClick
{
	#region Public methods

	public override void OnClick ()
	{
		foreach (GameObject SceneObject in Object.FindObjectsOfType<GameObject>())
		{
			Destroy (SceneObject);
		}

		ClickedOnResume.myClickedOnResume = false;
		gameObject.transform.parent.gameObject.SetActive (false);
		SceneManager.LoadScene ("Scenes/MainMenu");


		#endregion
	}

}
