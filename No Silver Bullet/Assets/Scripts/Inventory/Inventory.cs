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
	[SerializeField] GameObject myPlayer;
	[SerializeField] List<GameObject> myItemList;

	#endregion

	#region Public methods

	public void AddItemToFirstEmptyCarryingSlot (int aItemIndex)
	{
		bool addedItem = false;
		for (int i = 0; i < myCarryingSlots.transform.childCount && addedItem == false; i++)
		{
			InventorySlot slot = myCarryingSlots.GetChild (i).GetComponent<InventorySlot> ();

			if (slot.ContainedItem == null)
			{
				GameObject item = Instantiate (myItemList [aItemIndex]);
				slot.AddItem (item);
				addedItem = true;
			}
		}
	}

	public void LoadInventory (SerializableDictionary<int, int> aInformation)
	{
		for (int i = 0; i < myEquippedSlots.transform.childCount; i++)
		{
			InventorySlot slot = myEquippedSlots.GetChild (i).GetComponent<InventorySlot> ();
			int itemID;

			if (aInformation.TryGetValue (slot.UniqueID, out itemID))
			{
				GameObject item = Instantiate (myItemList [itemID]);
				slot.AddItem (item);
			}
		}

		for (int i = 0; i < myCarryingSlots.transform.childCount; i++)
		{
			InventorySlot slot = myCarryingSlots.GetChild (i).GetComponent<InventorySlot> ();
			int itemID;

			if (aInformation.TryGetValue (slot.UniqueID, out itemID))
			{
				GameObject item = Instantiate (myItemList [itemID]);
				slot.AddItem (item);
			}
		}
	}

	public SerializableDictionary<int, int> InventoryInformation ()
	{
		SerializableDictionary<int, int> inventoryInfo = new SerializableDictionary<int, int> ();

		foreach (Transform slotTransform in myEquippedSlots)
		{
			GameObject item = slotTransform.GetComponent<InventorySlot> ().ContainedItem;

			if (item != null)
			{

				int slotID = slotTransform.GetComponent<InventorySlot> ().UniqueID;
				int itemID = slotTransform.GetComponent<InventorySlot> ().ContainedItem.GetComponent<Item> ().UniqueID;
				inventoryInfo.Add (slotID, itemID);
			}
		}

		foreach (Transform slotTransform in myCarryingSlots)
		{
			GameObject item = slotTransform.GetComponent<InventorySlot> ().ContainedItem;

			if (item != null)
			{
				int slotID = slotTransform.GetComponent<InventorySlot> ().UniqueID;
				int itemID = slotTransform.GetComponent<InventorySlot> ().ContainedItem.GetComponent<Item> ().UniqueID;
				inventoryInfo.Add (slotID, itemID);
			}
		}

		return inventoryInfo;
	}

	public void HasChanged ()
	{
		float totalDamage = 0;
		float totalHealth = 0;
		float totalHealthRegeneration = 0;

		foreach (Transform slotTransform in myEquippedSlots)
		{
			GameObject item = slotTransform.GetComponent<InventorySlot> ().ContainedItem;

			if (item != null)
			{
				Item itemInfo = item.GetComponent<Item> ();
				totalDamage += itemInfo.Damage;
				totalHealth += itemInfo.Health;
				totalHealthRegeneration += itemInfo.HealthRegeneration;
			}
		}

		myPlayer.GetComponent<PlayerController> ().UpdateStats (totalDamage, totalHealth, totalHealthRegeneration);
	}

	#endregion

	#region Private methods

	private void Start ()
	{
		HasChanged ();
	}

	#endregion
}