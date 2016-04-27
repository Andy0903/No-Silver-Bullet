using UnityEngine;
using System.Collections;

public class MusicObjectController : MonoBehaviour 
{
	#region Member variables
	public AudioClip myAudioClip;
	private AudioClip myPreviousGlobalClip;
	private BoxCollider2D myBoxCollider;
	private bool mySwappedMusic = false;
	#endregion

	#region Private methods
	private void Awake () 
	{
		myBoxCollider = GetComponent<BoxCollider2D> ();
	}

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
