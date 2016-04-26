using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	#region Member variables

	public int myStartingHealth;
	private int myCurrentHealth;
	public Slider myHealthSlider;
	public Image myDamageImage;
	public float myFlashSpeed;
	public Color myFlashColor = new Color (1f, 0f, 0f, 0f);

	private bool myIsGettingDamanged;
	private float myHealthToBeRegenerated;

	#endregion

	#region Public methods

	public void TakeDamage (int aDamage)
	{
		myIsGettingDamanged = true;
		myCurrentHealth -= aDamage;
	}

	#endregion

	#region Private methods

	private void Awake ()
	{
		myCurrentHealth = myStartingHealth;
	}

	private void Update ()
	{
		UpdateDamageFlash ();
		RegenerateHealth ();
		myHealthSlider.value = myCurrentHealth;
	}

	private void UpdateDamageFlash ()
	{
		if (myIsGettingDamanged == true)
		{
			myDamageImage.color = myFlashColor;
		}
		else
		{
			myDamageImage.color = Color.Lerp (myDamageImage.color, Color.clear, myFlashSpeed * Time.deltaTime);
		}
		myIsGettingDamanged = false;
	}

	private void RegenerateHealth ()
	{
		const float HealthRegeneration = 3f;
		float healthRegenerationRate = HealthRegeneration;	//TODO add items that give more hp reg.

		if (myCurrentHealth < myStartingHealth)
		{
			float healthThisFrame = healthRegenerationRate * Time.deltaTime;
			myHealthToBeRegenerated += healthThisFrame;

			if (myHealthToBeRegenerated >= 1)
			{
				myHealthToBeRegenerated += 0.5f;
				myCurrentHealth += (int)myHealthToBeRegenerated; 
				myHealthToBeRegenerated = 0;

				if (myCurrentHealth > myStartingHealth)
				{
					myCurrentHealth = myStartingHealth;
				}
			}
		}
	}

	#endregion
}
