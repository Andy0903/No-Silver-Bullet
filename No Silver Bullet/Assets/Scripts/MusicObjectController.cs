using UnityEngine;
using System.Collections;

public class MusicObjectController : MonoBehaviour 
{
	#region Member variables
	//public int myRange; //Not used anymore
	public AudioClip myAudioClip;
	private AudioClip myPreviousGlobalClip;
	private BoxCollider2D myBoxCollider;
	private GameObject myTarget;
	private bool mySwappedMusic = false;
	#endregion

	#region Private methods
	private void Awake () 
	{
		myBoxCollider = GetComponent<BoxCollider2D> ();
		myTarget = GameObject.FindGameObjectWithTag ("Player"); //Sets the target
	}

	/*private void Update () 
	{
		float distanceFromTarget = Vector3.Distance (transform.position, myTarget.transform.position);

		if (distanceFromTarget < myRange && mySwappedMusic == false)
		{
			myPreviousGlobalClip = SoundManager.instance.myMusicSource.clip;
			SoundManager.instance.ChangeBGMusic (myAudioClip);
			mySwappedMusic = true;
		}
		else if (distanceFromTarget > myRange && mySwappedMusic == true)
		{
			SoundManager.instance.ChangeBGMusic (myPreviousGlobalClip);
			mySwappedMusic = false;
		}
	}*/

	private void OnTriggerEnter2D(Collider2D aCollider)
	{
		if (aCollider.tag == "Player" && mySwappedMusic == false)
		{
			myPreviousGlobalClip = SoundManager.instance.myMusicSource.clip;
			SoundManager.instance.ChangeBGMusic (myAudioClip);
			mySwappedMusic = true;
		}
	}

	private void OnTriggerExit2D(Collider2D aCollider)
	{
		if (aCollider.tag == "Player" && mySwappedMusic == true)
		{
			SoundManager.instance.ChangeBGMusic (myPreviousGlobalClip);
			mySwappedMusic = false;
		}
	}
	#endregion
}
