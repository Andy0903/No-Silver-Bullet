using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	#region Member variables

	public AudioClip[] myTakingDamageGrunts;
	private float myCurrentHealth;
	public Slider myHealthSlider;
	public Image myDamageImage;
	public float myFlashSpeed;
	public Color myFlashColor = new Color (1f, 0f, 0f, 0f);

	private bool myIsGettingDamanged;
	private float myHealthToBeRegenerated;

	[SerializeField] PlayerController myPlayerController;

	[SerializeField] GameObject myGameOverCanvas;

	#endregion

	#region Properties

	public bool IsAlive
	{
		get;
		private set;
	}

	public float MaxHealth
	{
		get;
		set;
	}

	public float HealthRegeneration
	{
		get;
		set;
	}

	#endregion

	#region Public methods

	public void TakeDamage (int aDamage)
	{
		myIsGettingDamanged = true;
		myCurrentHealth -= aDamage;
		SoundManager.instance.RandomizeSfx (myTakingDamageGrunts);
	}

	#endregion

	#region Private methods

	private void Awake ()
	{
		IsAlive = true;
		myCurrentHealth = MaxHealth;
	}

	private void Update ()
	{
		UpdateDamageFlash ();
		RegenerateHealth ();
		myHealthSlider.value = myCurrentHealth;

		if (myCurrentHealth <= 0 && IsAlive == true)
		{
			Death ();
		}
	}

	private void Death ()
	{
		IsAlive = false;
		Time.timeScale = 0;
		myGameOverCanvas.SetActive (!IsAlive);
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
		float healthRegenerationRate = HealthRegeneration;

		if (myCurrentHealth < MaxHealth)
		{
			float healthThisFrame = healthRegenerationRate * Time.deltaTime;
			myHealthToBeRegenerated += healthThisFrame;

			if (myHealthToBeRegenerated >= 1)
			{
				myHealthToBeRegenerated += 0.5f;
				myCurrentHealth += (int)myHealthToBeRegenerated; 
				myHealthToBeRegenerated = 0;
			}
		}

		if (myCurrentHealth > MaxHealth)
		{
			myCurrentHealth = MaxHealth;
		}
	}

	#endregion
}
