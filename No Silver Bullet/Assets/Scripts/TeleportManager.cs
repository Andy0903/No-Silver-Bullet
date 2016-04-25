using UnityEngine;
using System.Collections;

public class TeleportManager : MonoBehaviour
{
	#region Member variables

	public int myTeleportIndex;
	const float Cooldown = 0.1f;
	float myTimer;

	#endregion

	#region Private mehtods

	private void Awake ()
	{
		myTimer = 0;
	}

	private void OnTriggerEnter2D (Collider2D aOther)
	{
		if (aOther.tag == "Player")
		{
			GameObject[] teleports = GameObject.FindGameObjectsWithTag ("Teleport");

			foreach (GameObject teleporter in teleports)
			{
				TeleportManager teleportManager = teleporter.GetComponent<TeleportManager> ();

				if (teleportManager.myTeleportIndex == myTeleportIndex && teleporter != gameObject)
				{
					if (myTimer <= Time.time)
					{
						aOther.transform.position = teleporter.transform.position;
						myTimer = Time.time + Cooldown;
						teleportManager.myTimer = myTimer;
					}
				}
			}
		}
	}

	#endregion
}
