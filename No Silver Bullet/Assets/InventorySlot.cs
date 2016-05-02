using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
	#region Properties

	public GameObject Item
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

	#endregion

	#region Public methods

	public void OnDrop (PointerEventData aEventData)
	{
		if (Item == null)
		{
			DragHandler.myItemBeingDragged.transform.SetParent (gameObject.transform);
			ExecuteEvents.ExecuteHierarchy<IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
		}
	}

	#endregion
}
