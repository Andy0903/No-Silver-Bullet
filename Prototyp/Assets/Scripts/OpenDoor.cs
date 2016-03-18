using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
	Animator myAnimator;

	void Awake()
	{
		myAnimator = GetComponentInParent<Animator>();
	}
		
	void OnTriggerEnter2D(Collider2D aOther)
	{
		myAnimator.SetTrigger ("Open");
	}

	void OnTriggerExit2D(Collider2D aOther)
	{
		myAnimator.SetTrigger ("Closed");
	}

}
