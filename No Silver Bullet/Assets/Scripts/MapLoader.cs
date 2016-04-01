using UnityEngine;
using System.Collections;
using TiledSharp;
using System.Collections.Generic;

public class MapLoader : MonoBehaviour
{
	#region Member variables

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
					GameObject toInstantiate = myTiles [index];

					GameObject instance = Instantiate (toInstantiate, new Vector2 (tile.X, -tile.Y), Quaternion.identity) as GameObject;
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

				switch (index)
				{
				default:
					throw new System.Exception ("Missing case for objectIndex");
				}
			}
		}
	}

	#endregion
}
