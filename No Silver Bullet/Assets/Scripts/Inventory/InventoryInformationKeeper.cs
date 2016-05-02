using UnityEngine;
using System.Collections;

public class InventoryInformationKeeper
{
	#region Member variables

	public SerializableDictionary<int, int> myInventoryInformation;

	#endregion

	#region Constructors

	public InventoryInformationKeeper ()
	{
		myInventoryInformation = new SerializableDictionary<int, int> ();	//Key - SlotUniqueID, Value - ItemUniqueID
	}

	#endregion
}
