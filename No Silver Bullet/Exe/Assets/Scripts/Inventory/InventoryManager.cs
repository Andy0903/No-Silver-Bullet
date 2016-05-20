using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
	#region Member variables

	bool myIsActive;
	public GameObject myCanvas;

	#endregion

	#region Private methods

	private void Start ()
	{
		myIsActive = false;
		myCanvas.SetActive (myIsActive);
	}

	private void Update ()
	{
		ReadInput ();
	}

	private void ReadInput ()
	{
		if (Input.GetKeyDown (KeyCode.I))
		{
			myIsActive = !myIsActive;
			myCanvas.SetActive (myIsActive);
		}
	}

	#endregion

}
