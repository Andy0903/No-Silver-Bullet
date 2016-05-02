using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour, IHasChanged
{
	#region Member variables

	[SerializeField] Transform myEquippedSlots;
	[SerializeField] Transform myCarryingSlots;
	List<int> myItemIDs;
	Dictionary<Item.ItemTypes, Item> myEquppiedItems;
	List<Item> myItems;

	#endregion

	#region Public methods

	public void HasChanged ()
	{
		foreach (Transform slotTransform in myEquippedSlots)
		{
			GameObject item = slotTransform.GetComponent<InventorySlot> ().Item;

			if (item != null)
			{
			}
		}
	}

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

	private void Start ()
	{
		HasChanged ();
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