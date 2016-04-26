using UnityEngine;
using System.Collections;

public class MusicObjectController : MonoBehaviour 
{
	#region Member variables
	public int myRange;
	public AudioClip myAudioClip;
	private AudioClip myPreviousGlobalClip;
	private GameObject myTarget;
	private bool mySwappedMusic = false;
	#endregion

	#region Private methods
	private void Awake () 
	{
		myTarget = GameObject.FindGameObjectWithTag ("Player"); //Sets the target
	}

	private void Update () 
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
			mySwappedMusic == false;
		}
	}
	#endregion
}
