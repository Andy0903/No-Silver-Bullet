using UnityEngine;
using System.Collections;

public class MovementHover : MonoBehaviour
{
	#region Member variables

	Vector2 mySineWavePosition;

	#endregion

	#region Private methods

	private void Start ()
	{
		mySineWavePosition = Vector2.zero;
	}

	private void Update ()
	{
		Hover ();
	}

	private Vector3 CalculateHoverPosition (Vector2 aAmplitude, Vector2 aFrequency)
	{
		float posX = mySineWavePosition.x;
		float posY = mySineWavePosition.y;

		posX += aFrequency.x * Time.deltaTime;
		posY += aFrequency.y * Time.deltaTime;

		// Subtracts all full cycles to avoid overflows.
		posX -= (float)(Mathf.Floor (posX / (Mathf.PI * 2)) * Mathf.PI * 2);
		posY -= (float)(Mathf.Floor (posY / (Mathf.PI * 2)) * Mathf.PI * 2);

		mySineWavePosition = new Vector2 (posX, posY);

		return new Vector3 (aAmplitude.x * (float)Mathf.Sin (posX), aAmplitude.y * (float)Mathf.Sin (posY), 0);
	}

	private void Hover (/*Vector2 aPosition*/)
	{
		//Vector2 amplitude = new Vector2 (WindowManager.WindowWidth * 0.01f, WindowManager.WindowHeight * 0.025f);
		Vector2 amplitude = new Vector2 (0.01f, 0.025f);
		Vector2 frequency = new Vector2 (1f, 2f);

		Vector3 newPosition = CalculateHoverPosition (amplitude, frequency);

		//gameObject.transform.position = aPosition + newPosition;
		gameObject.transform.position = gameObject.transform.position + newPosition;
	}

	#endregion

}
