using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	#region Member variables

	public Transform myTarget;
	private float mySmoothing = 5f;
	private Vector3 myOffset;

	#endregion

	#region Private methods

	private void Start ()
	{
		transform.position = new Vector3 (myTarget.transform.position.x, myTarget.transform.position.y, transform.position.z);
		myOffset = transform.position - myTarget.position;
	}

	private void Update ()
	{
		Vector3 targetCameraPosition = myTarget.position + myOffset;
		transform.position = Vector3.Lerp (transform.position, targetCameraPosition, mySmoothing * Time.deltaTime);
	}

	#endregion
}
