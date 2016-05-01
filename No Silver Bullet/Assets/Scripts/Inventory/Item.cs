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
		Neck
	}

	#endregion

	#region Properties

	public ItemTypes ItemType
	{
		get;
		private set;
	}

	#endregion

	#region Private methods

	void Start ()
	{

	}

	void Update ()
	{

	}

	#endregion

}
