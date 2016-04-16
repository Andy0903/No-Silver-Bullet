using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	void Awake ()
	{
		Object.DontDestroyOnLoad (gameObject);
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F))
		{
			Application.LoadLevel ("Scenes/Act3");
		}

		if (Input.GetKeyDown (KeyCode.E))
		{
			Application.LoadLevel ("Scenes/Scene");
		}
	}
}
