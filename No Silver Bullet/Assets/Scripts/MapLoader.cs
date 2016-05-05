using UnityEngine;
using System.Collections;
using TiledSharp;
using System.Collections.Generic;
using System;

public class MapLoader : MonoBehaviour
{
	#region Member variables

	public string myTMXfileName;
	public GameObject[] myTiles;
	public GameObject[] myTmxObjects;
	TmxMap myMap;
	Transform myBoardHolder;
	Dictionary<int, int> myTileGidToIndex;
	GameObject myPlayer;

	#endregion

	#region Private methods

	private void Awake ()
	{ 
		myBoardHolder = new GameObject ("Board").transform;
		myTileGidToIndex = new Dictionary<int,int> ();
		myMap = new TmxMap ("Assets/Tmx/" + myTMXfileName + ".tmx");
		myPlayer = GameObject.FindGameObjectWithTag ("Player");

		IndexateTiles ();
		CreateBoardTmxObjects ();
		CreateBoardTiles ();
	}

	private void IndexateTiles ()
	{
		foreach (TmxTileset tileSet in myMap.Tilesets)
		{
			foreach (TmxTilesetTile tile in tileSet.Tiles)
			{
				if (tile.Properties.ContainsKey ("UnityTileIndex"))
				{
					myTileGidToIndex [tileSet.FirstGid + tile.Id] = int.Parse (tile.Properties ["UnityTileIndex"]);
				}
			}
		}
	}

	private void CreateBoardTiles ()
	{
		foreach (TmxLayer layer in myMap.Layers)
		{
			foreach (TmxLayerTile tile in layer.Tiles)
			{
				if (myTileGidToIndex.ContainsKey (tile.Gid))
				{
					int index = myTileGidToIndex [tile.Gid];
					GameObject toInstantiate = myTiles [index];

					if (toInstantiate == null)
					{
						Debug.Break ();
					}

					GameObject instance = Instantiate (toInstantiate, new Vector2 (tile.X, -tile.Y), Quaternion.identity) as GameObject;

					if (tile.HorizontalFlip == true && tile.DiagonalFlip == true)
					{
						instance.transform.localRotation = Quaternion.Euler (0, 0, 270);
					}
					else if (tile.HorizontalFlip == true && tile.VerticalFlip == true)
					{
						instance.transform.localRotation = Quaternion.Euler (0, 0, 180);
					}
					else if (tile.VerticalFlip == true && tile.DiagonalFlip == true)
					{
						instance.transform.localRotation = Quaternion.Euler (0, 0, 90);
					}

					instance.transform.SetParent (myBoardHolder);
				}
			}
		}
	}

	private void CreateBoardTmxObjects ()
	{
		foreach (TmxObjectGroup objectGroup in myMap.ObjectGroups)
		{
			foreach (TmxObject tmxObject in objectGroup.Objects)
			{
				int index = int.Parse (tmxObject.Properties ["ObjectIndex"]);

				if (IsForCompletedQuest (tmxObject))
				{
					continue;
				}

				GameObject toInstantiate = myTmxObjects [index];
				GameObject instance = Instantiate (toInstantiate,
					                      new Vector2 ((int)tmxObject.X / myMap.TileWidth, (int)-tmxObject.Y / myMap.TileHeight),
					                      Quaternion.identity) as GameObject;
				instance.transform.SetParent (myBoardHolder);

				SceneLoader loadAct = instance.GetComponent<SceneLoader> ();
				TeleportManager teleport = instance.GetComponent<TeleportManager> ();

				if (tmxObject.Properties.ContainsKey ("QuestIndex"))
				{
					QuestLinker questLinker = instance.GetComponent<QuestLinker> ();
					ProgressTracker.Quests quest = (ProgressTracker.Quests)Enum.Parse (typeof(ProgressTracker.Quests), tmxObject.Properties ["QuestIndex"]);       
					questLinker.myBelongsToQuest = quest;
				}

				switch (index)
				{
				case 0:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 1:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 2:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 3:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 4:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 5:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 6:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 7:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 8:
					loadAct.myLeadsToSceneName = "Scene";
					break;
				case 9:
					loadAct.myLeadsToSceneName = "Act2";
					break;
				case 10:
					loadAct.myLeadsToSceneName = "Act3";
					break;
				case 11:
					loadAct.myLeadsToSceneName = "Act4";
					break;
				case 12:
					loadAct.myLeadsToSceneName = "Act5";
					break;
				case 13:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 14:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 15:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 16:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
					break;
				case 24:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 25:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 26:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 27:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 28:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 29:
				case 30:
				case 31:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 32:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 33:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 34:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 35:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 36:
					//
					break;
				case 37:
				case 38:
				case 39:
				case 40:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 41:
					teleport.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				default:
					throw new System.Exception ("Missing case for objectIndex");
				}
			}
		}


	}

	private bool IsForCompletedQuest (TmxObject tmxObject)
	{
		if (tmxObject.Properties.ContainsKey ("QuestIndex"))
		{
			try
			{
				ProgressTracker.Quests quest = (ProgressTracker.Quests)Enum.Parse (typeof(ProgressTracker.Quests), tmxObject.Properties ["QuestIndex"]);        
				if (Enum.IsDefined (typeof(ProgressTracker.Quests), quest))
				{
					return myPlayer.GetComponent<PlayerController> ().myProgressTracker.GetQuestStatus (quest);
				}
			}
			catch (ArgumentException)
			{
			}
		}
		return false;
	}

	#endregion
}

