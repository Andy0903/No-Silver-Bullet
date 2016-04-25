using UnityEngine;
using System.Collections;
using TiledSharp;
using System.Collections.Generic;

public class MapLoader : MonoBehaviour
{
	#region Member variables

	public string myTMXfileName;
	public GameObject[] myTiles;
	public GameObject[] myTmxObjects;
	TmxMap myMap;
	Transform myBoardHolder;
	Dictionary<int, int> myTileGidToIndex;

	#endregion

	#region Private methods

	private void Awake ()
	{
		myBoardHolder = new GameObject ("Board").transform;
		myTileGidToIndex = new Dictionary<int,int> ();
		myMap = new TmxMap ("Assets/Tmx/" + myTMXfileName + ".tmx");

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
				GameObject toInstantiate = myTmxObjects [index];
				GameObject instance = Instantiate (toInstantiate, new Vector2 ((int)tmxObject.X / myMap.TileWidth, (int)-tmxObject.Y / myMap.TileHeight), Quaternion.identity) as GameObject;
				instance.transform.SetParent (myBoardHolder);


				SceneLoader loadAct = instance.GetComponent<SceneLoader> ();
				switch (index)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
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
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
					break;

				//default:
				//throw new System.Exception ("Missing case for objectIndex");
				}

			}
		}
	}

	#endregion
}
