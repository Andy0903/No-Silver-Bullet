using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour
{
	#region private methods

	private void Awake ()
	{
		Object.DontDestroyOnLoad (gameObject);
	}

	#endregion

}
