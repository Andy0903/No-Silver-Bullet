using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	#region Member variables

	public AudioSource myFirstEfxSource;
	public AudioSource mySecondEfxSource;
	public AudioSource myThirdEfxSource;
	public AudioSource myMusicSource;
	public static SoundManager instance = null;
	//Rename to myInstance?
	public float myLowPitchRange = .95f;
	public float myHighPitchRange = 1.05f;

	#endregion

	#region Private methods

	private void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	#endregion

	#region Public methods

	public void ChangeBGMusic (AudioClip aClip)
	{
		myMusicSource.clip = aClip;

		myMusicSource.Play ();
	}

	public void PlaySingle (AudioClip aClip)
	{
		if (myFirstEfxSource.isPlaying == false)
		{
			myFirstEfxSource.clip = aClip;

			myFirstEfxSource.Play ();
		}
		else if (mySecondEfxSource.isPlaying == false)
		{
			mySecondEfxSource.clip = aClip;
			mySecondEfxSource.Play ();
		}
		else if (myThirdEfxSource.isPlaying == false)
		{
			myThirdEfxSource.clip = aClip;
			myThirdEfxSource.Play ();
		}
	}

	public void RandomizeSfx (params AudioClip[] aClips)
	{
		int randomIndex = Random.Range (0, aClips.Length);
		float randomPitch = Random.Range (myLowPitchRange, myHighPitchRange);

		myFirstEfxSource.pitch = randomPitch;
		mySecondEfxSource.pitch = randomPitch;

		if (myFirstEfxSource.isPlaying == false)
		{
			myFirstEfxSource.clip = aClips [randomIndex];

			myFirstEfxSource.Play ();
		}
		else if (mySecondEfxSource.isPlaying == false)
		{
			mySecondEfxSource.clip = aClips [randomIndex];

			mySecondEfxSource.Play ();
		}
		else if (myThirdEfxSource.isPlaying == false)
		{
			myThirdEfxSource.clip = aClips [randomIndex];

			myThirdEfxSource.Play ();
		}
	}

	#endregion

}
