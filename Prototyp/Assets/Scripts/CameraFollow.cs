using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
	{
		public Transform myTarget;
		public float mySmoothing = 5f;

		Vector3 myOffset;

		void Start()
		{
			transform.position = new Vector3 (myTarget.transform.position.x, myTarget.transform.position.y, transform.position.z);
			myOffset = transform.position - myTarget.position;
		}

		void FixedUpdate()
		{
		Vector3 targetCameraPosition = myTarget.position + myOffset;
			transform.position = Vector3.Lerp(transform.position, targetCameraPosition, mySmoothing * Time.deltaTime);
		}
	}

