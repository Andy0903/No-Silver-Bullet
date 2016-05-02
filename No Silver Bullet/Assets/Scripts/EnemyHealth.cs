using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	#region Member variables

	public AudioClip[] myTakingDamageGrunts;
	public float myStartingHealth;
	private float myCurrentHealth;

	#endregion

	#region Properties

	public float CurrentHealth
	{
		get { return myCurrentHealth; }
		private set { myCurrentHealth = myStartingHealth; }
	}

	#endregion

	#region Public methods

	public void TakeDamage (float aDamage)
	{
		//Fixes null errors when playing sounds
		if (myTakingDamageGrunts.Length > 0)
		{
			SoundManager.instance.RandomizeSfx (myTakingDamageGrunts);
		}
		myCurrentHealth -= aDamage;
	}

	#endregion

	#region Private methods

	private void Awake ()
	{
		myCurrentHealth = myStartingHealth;
	}

	#endregion
}
