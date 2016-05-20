using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
	#region Member variables

	public enum ItemTypes
	{
		Weapon,
		Chest,
		Feet,
		Hands,
		Neck,
		Quest
	}

	[SerializeField] private int myUniqueID;
	[SerializeField] private ItemTypes myItemType;
	[SerializeField] private string myName;
	[SerializeField] private float myHealthRegeneration;
	[SerializeField] private float myDamage;
	[SerializeField] private float myHealth;

	#endregion

	#region Properties

	public int UniqueID
	{
		get { return myUniqueID; }
		private set { myUniqueID = value; }
	}

	public ItemTypes ItemType
	{
		get { return myItemType; }
		private set { myItemType = value; }
	}

	public string Name
	{
		get { return myName; }
		private set { myName = value; }
	}

	public float HealthRegeneration
	{
		get { return myHealthRegeneration; }
		private set { myHealthRegeneration = value; }
	}

	public float Damage
	{
		get{ return myDamage; }
		private set { myDamage = value; }
	}

	public float Health
	{
		get { return myHealth; }
		private set { myHealth = value; }
	}

	#endregion

}
