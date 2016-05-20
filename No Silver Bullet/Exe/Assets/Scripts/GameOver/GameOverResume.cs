using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverResume : MenuButtonClick
{
	#region Public methods

	public override void OnClick ()
	{
		foreach (GameObject SceneObject in Object.FindObjectsOfType<GameObject>())
		{
			Destroy (SceneObject);
		}
		gameObject.transform.parent.gameObject.SetActive (false);
		SceneManager.LoadScene ("Scenes/Bootstrap");


		#endregion
	}

}