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
			if (SceneObject.CompareTag ("Player"))
			{
				Debug.Log ("Player");
			}

			Destroy (SceneObject);
		}

		ClickedOnResume.myClickedOnResume = true;
		gameObject.transform.parent.gameObject.SetActive (false);
		SceneManager.LoadScene ("Scenes/Bootstrap");


		#endregion
	}

}