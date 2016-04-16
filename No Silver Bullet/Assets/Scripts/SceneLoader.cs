using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	#region Member variables

	public string myLeadsToSceneName;
	public Vector2 myStartPosition;
	float myCooldown;
	float myTimer;

	#endregion

	#region Private methods

	private void Awake ()
	{
		myTimer = 0;
		myCooldown = 2f;
	}

	private void OnTriggerEnter2D (Collider2D aOther)
	{
		if (aOther.tag == "Player")
		{
			if (myTimer <= Time.time)
			{
				aOther.transform.position = new Vector3 (myStartPosition.x, myStartPosition.y, 0);

				myTimer = Time.time + myCooldown;
				Application.LoadLevel ("Scenes/" + myLeadsToSceneName);
			}
		}
	}

	#endregion
}
