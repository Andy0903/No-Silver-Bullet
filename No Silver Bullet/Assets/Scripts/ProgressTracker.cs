using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressTracker
{
	#region Member variables

	public enum Quests
	{
		DefeatAct1Boss,
		DefeatAct2Boss,
		DefeatAct3Boss,
		DefeatAct4Boss,
	}

	private Dictionary<Quests, bool> myProgress;

	#endregion

	#region Private methods

	public ProgressTracker ()
	{
		myProgress = new Dictionary<Quests, bool> ();
	}

	public bool GetQuestStatus (Quests aQuest)
	{
		bool value;
		myProgress.TryGetValue (aQuest, out value);
		return value;
	}

	public void SetQuestStatus (Quests aQuest, bool aState)
	{
		myProgress [aQuest] = aState;
		SavedGame.SaveGame ();
	}

	#endregion

}
