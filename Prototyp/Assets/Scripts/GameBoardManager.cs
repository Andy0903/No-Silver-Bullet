using UnityEngine;
using System.Collections;
using TiledSharp;
using System.Collections.Generic;

public class GameBoardManager : MonoBehaviour
{
	public GameObject[] myTiles;
	public GameObject[] myTmxObjects;

	TmxMap myMap;
	Transform myBoardHolder;
	Dictionary<int, int> myTileGidToIndex;

	private void Awake ()
	{
		myBoardHolder = new GameObject ("Board").transform;
		myTileGidToIndex = new Dictionary<int, int> ();
		myMap = new TmxMap ("Assets/Tmx/World.tmx");


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
					GameObject toInstanceiate = myTiles [index];

					GameObject instance = Instantiate (toInstanceiate, new Vector3 (tile.X, -tile.Y, 0), Quaternion.identity) as GameObject;
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
				GameObject toInstanceiate = myTmxObjects [index];

				GameObject instance = Instantiate (toInstanceiate, new Vector3 (((int)tmxObject.X / myMap.TileWidth), ((int)-tmxObject.Y / myMap.TileHeight), 0), Quaternion.identity) as GameObject;
				instance.transform.SetParent (myBoardHolder);

				switch (index)
				{
				case 0:
					TeleportPlayer teleportPlayer = instance.GetComponent<TeleportPlayer> ();
					teleportPlayer.myTeleportIndex = int.Parse (tmxObject.Properties ["TeleportIndex"]);
					break;
				case 1:
					break;
				default:
					throw new System.Exception ("missing case for object index");
				}
			}
		}
	}
}
