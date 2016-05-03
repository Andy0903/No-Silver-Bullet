using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	#region Member variables

	[SerializeField] int myUniqueID;

	#endregion

	#region Properties

	public GameObject ContainedItem
	{
		get
		{
			if (gameObject.transform.childCount > 0)
			{
				return transform.GetChild (0).gameObject;
			}
			return null;
		}
	}

	public int UniqueID
	{
		get { return myUniqueID; }
		private set { myUniqueID = value; }
	}


	#endregion

	#region Public methods

	public void AddItem (GameObject aItem)
	{
		if (ContainedItem == null)
		{
			aItem.transform.SetParent (gameObject.transform, false);
			ExecuteEvents.ExecuteHierarchy<IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
		}
	}

	public void OnDrop (PointerEventData aEventData)
	{
		if (ContainedItem == null)
		{
			Item.ItemTypes type = DragHandler.myItemBeingDragged.GetComponent<Item> ().ItemType;

			if (gameObject.CompareTag ("Slot") || gameObject.CompareTag (type.ToString () + "Slot"))
			{
				DragHandler.myItemBeingDragged.transform.SetParent (gameObject.transform);
				ExecuteEvents.ExecuteHierarchy<IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
			}
		}
	}

	#endregion
}
