using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
	#region Member variables

	List<int> myItemIDs;
	Dictionary<Item.ItemTypes, Item> myEquppiedItems;
	List<Item> myItems;

	#endregion

	#region Private methods

	private void Awake ()
	{
		if (myItemIDs == null)
		{
			myItemIDs = new List<int> ();
		}

		if (myEquppiedItems == null)
		{
			myEquppiedItems = new Dictionary<Item.ItemTypes, Item> ();
		}

		if (myItems == null)
		{
			myItems = new List<Item> ();
		}
	}

	private void EquipItem (Item aItem)
	{
		Item oldEquippedItem = myEquppiedItems [aItem.ItemType];
		myEquppiedItems [aItem.ItemType] = aItem;

		RemoveItemFromBag (aItem);
		PutItemInBag (oldEquippedItem);

	}

	private void PutItemInBag (Item aItem)
	{
		if (aItem != null)
		{
			if (myItems.Count < myItems.Capacity)
			{
				myItems.Add (aItem);
			}
		}
	}

	private void RemoveItemFromBag (Item aItem)
	{
		if (aItem != null)
		{
			myItems.Remove (aItem);
		}
	}

	#endregion
}
