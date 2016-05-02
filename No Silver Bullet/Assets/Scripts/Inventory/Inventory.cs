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
	//List<int> myItemIDs;
	//Dictionary<Item.ItemTypes, Item> myEquppiedItems;
	//List<Item> myItems;
	[SerializeField] GameObject myPlayer;

	#endregion

	#region Public methods

	public void HasChanged ()
	{
		float totalDamage = 0;
		float totalHealth = 0;
		float totalHealthRegeneration = 0;
		myPlayer.GetComponent<PlayerController> ().myInventorySetup = new InventoryInformationKeeper ();
		//myItemIDs = new List<int> ();

		foreach (Transform slotTransform in myEquippedSlots)
		{
			GameObject item = slotTransform.GetComponent<InventorySlot> ().ContainedItem;

			if (item != null)
			{
				Item itemInfo = item.GetComponent<Item> ();
				totalDamage += itemInfo.Damage;
				totalHealth += itemInfo.Health;
				totalHealthRegeneration += itemInfo.HealthRegeneration;
				//myItemIDs.Add (item.GetComponent<Item> ().UniqueID);

				int slotID = slotTransform.GetComponent<InventorySlot> ().UniqueID;
				int itemID = slotTransform.GetComponent<InventorySlot> ().ContainedItem.GetComponent<Item> ().UniqueID;
				myPlayer.GetComponent<PlayerController> ().myInventorySetup.myInventoryInformation.Add (slotID, itemID);
			}
		}

		myPlayer.GetComponent<PlayerController> ().UpdateStats (totalDamage, totalHealth, totalHealthRegeneration);

		foreach (Transform slotTransform in myCarryingSlots)
		{
			GameObject item = slotTransform.GetComponent<InventorySlot> ().ContainedItem;

			if (item != null)
			{
				int slotID = slotTransform.GetComponent<InventorySlot> ().UniqueID;
				int itemID = slotTransform.GetComponent<InventorySlot> ().ContainedItem.GetComponent<Item> ().UniqueID;
				myPlayer.GetComponent<PlayerController> ().myInventorySetup.myInventoryInformation.Add (slotID, itemID);
			}
		}

//		myPlayer.GetComponent<PlayerController> ().myItemIDs = myItemIDs;
	}

	#endregion

	#region Private methods

	private void Awake ()
	{
/*		if (myItemIDs == null)
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
		}*/
	}

	private void Start ()
	{
		HasChanged ();
	}

	private void EquipItem (Item aItem)
	{
/*		Item oldEquippedItem = myEquppiedItems [aItem.ItemType];
		myEquppiedItems [aItem.ItemType] = aItem;

		RemoveItemFromBag (aItem);
		PutItemInBag (oldEquippedItem);*/

	}

	private void PutItemInBag (Item aItem)
	{
/*		if (aItem != null)
		{
			if (myItems.Count < myItems.Capacity)
			{
				myItems.Add (aItem);
			}
		}*/
	}

	private void RemoveItemFromBag (Item aItem)
	{
/*		if (aItem != null)
		{
			myItems.Remove (aItem);
		}*/
	}

	#endregion
}