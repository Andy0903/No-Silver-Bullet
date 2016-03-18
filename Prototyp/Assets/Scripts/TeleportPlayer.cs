using UnityEngine;
using System.Collections;

public class TeleportPlayer : MonoBehaviour 
{
	public int myTeleportIndex;
	public float myCooldown;
	float myTimer;

	void Awake()
	{
		myTimer = 0;
	}

	void OnTriggerEnter2D(Collider2D aOther)
	{
		if (aOther.tag == "Player") 
		{
			GameObject[] teleports = GameObject.FindGameObjectsWithTag ("Teleport");

			foreach (GameObject teleport in teleports) 
			{
				TeleportPlayer teleportPlayer = teleport.GetComponent<TeleportPlayer> ();

				if (teleportPlayer.myTeleportIndex == myTeleportIndex && teleport != gameObject)
				{
					if (myTimer <= Time.time)
					{
						aOther.transform.position = teleport.transform.position;
						myTimer = Time.time + myCooldown;
						teleportPlayer.myTimer = myTimer;
					}
				}
			}
		}
	}
}
