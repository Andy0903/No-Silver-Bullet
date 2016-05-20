using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface IHasChanged : IEventSystemHandler
{
	void HasChanged ();
}
