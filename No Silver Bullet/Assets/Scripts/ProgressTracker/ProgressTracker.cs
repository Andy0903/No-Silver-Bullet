using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressTracker
{
	#region Member variables

	public enum Quests
	{
		None,

		DefeatAct1Boss,
		DefeatAct2Boss,
		DefeatAct3Boss,
		DefeatAct4Boss1,
		DefeatAct4Boss2,
		DefeatAct4Boss3,
		DefeatAct4Boss4,
		DefeatAct5Boss,

		ClearStartTown,
	}

	public SerializableDictionary<Quests, bool> myProgress;

	#endregion

	#region Constructors

	public ProgressTracker ()
	{
		myProgress = new SerializableDictionary<Quests, bool> ();
	}

	#endregion

	#region Private methods

	public bool GetQuestStatus (Quests aQuest)
	{
		bool value;
		myProgress.TryGetValue (aQuest, out value);
		return value;
	}

	public void SetQuestStatus (Quests aQuest, bool aState)
	{
		if (aQuest != Quests.None)
		{
			myProgress [aQuest] = aState;
			SavedGame.SaveGame ();
		}
	}

	#endregion

}
