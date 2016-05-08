using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region Member variables

	public static GameObject myItemBeingDragged;
	Vector3 myStarPosition;
	Transform myStartParent;
	Transform myStartGreatGrandparent;
	int myParentIndex;

	#endregion

	#region Public methods

	public void OnBeginDrag (PointerEventData aEventData)
	{
		myItemBeingDragged = gameObject;
		myStarPosition = gameObject.transform.position;
		myStartParent = gameObject.transform.parent;
		myStartGreatGrandparent = myStartParent.parent.parent;
		gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		myItemBeingDragged.GetComponent<LayoutElement> ().ignoreLayout = true;
		myItemBeingDragged.transform.SetParent (myStartGreatGrandparent);
	}

	public void OnDrag (PointerEventData aEventData)
	{
		gameObject.transform.position = Input.mousePosition;	
	}

	public void OnEndDrag (PointerEventData aEventData)
	{
		myItemBeingDragged.GetComponent<LayoutElement> ().ignoreLayout = false;
		gameObject.GetComponent<CanvasGroup> ().blocksRaycasts = true;

		if (gameObject.transform.parent == myStartParent)
		{
			gameObject.transform.position = myStarPosition;
		}
		else if (gameObject.transform.parent == myStartGreatGrandparent)
		{
			gameObject.transform.SetParent (myStartParent);
		}
		myItemBeingDragged = null;
	}

	#endregion
}
