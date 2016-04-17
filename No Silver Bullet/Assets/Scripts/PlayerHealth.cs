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

	bool myIsGettingDamanged;

	#endregion

	#region Public methods

	public void TakeDamage (int aDamage)
	{
		myIsGettingDamanged = true;
		myCurrentHealth -= aDamage;
		myHealthSlider.value = myCurrentHealth;
	}

	#endregion

	#region Private methods

	void Awake ()
	{
		myCurrentHealth = myStartingHealth;
	}

	void Update ()
	{
		UpdateDamageFlash ();
	}

	void UpdateDamageFlash ()
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

	#endregion
}
