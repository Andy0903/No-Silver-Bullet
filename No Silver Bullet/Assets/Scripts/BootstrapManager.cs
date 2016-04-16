using UnityEngine;
using System.Collections;

public class BootstrapManager : MonoBehaviour
{
	void Update ()
	{
		if (GameObject.FindGameObjectWithTag ("Player") && GameObject.FindGameObjectWithTag ("MainCamera"))
		{
			Application.LoadLevel ("Scenes/Scene");
		}
	}
}
