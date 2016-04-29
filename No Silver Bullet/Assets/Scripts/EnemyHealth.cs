using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	#region Member variables

	public AudioClip[] myTakingDamageGrunts;
	public int myStartingHealth;
	private int myCurrentHealth;

	#endregion

	#region Properties
	public int CurrentHealth
	{
		get { return myCurrentHealth; }
		private set { myCurrentHealth = myStartingHealth; }
	}
	#endregion

	#region Public methods

	public void TakeDamage (int aDamage)
	{
		SoundManager.instance.RandomizeSfx (myTakingDamageGrunts);
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
