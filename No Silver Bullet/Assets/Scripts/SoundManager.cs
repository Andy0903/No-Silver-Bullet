using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
	#region Member variables

	public AudioSource myEfxSource;
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
		myEfxSource.clip = aClip;

		myEfxSource.Play ();
	}

	public void RandomizeSfx (params AudioClip[] aClips)
	{
		int randomIndex = Random.Range (0, aClips.Length);
		float randomPitch = Random.Range (myLowPitchRange, myHighPitchRange);

		myEfxSource.pitch = randomPitch;

		myEfxSource.clip = aClips [randomIndex];

		myEfxSource.Play ();
	}

	#endregion

}
