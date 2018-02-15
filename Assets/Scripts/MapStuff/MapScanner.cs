using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapScanner : MonoBehaviour {

    private GameObject Map;
    private Tilemap Ground;
    private Tilemap Level1;
    void Start()
    {
        Map = gameObject;
        Ground = Map.transform.GetChild(0).GetComponent<Tilemap>();
        Level1 = Map.transform.GetChild(1).GetComponent<Tilemap>();
        SetLevel1TileInfo();
        SetGroundTileInfo();
        //bounds = tilemapBlock.cellBounds;
        //TileBase[] blockTiles = tilemapBlock.GetTilesBlock(tilemapBlock.cellBounds);
        //for (int x = 0; x < 200; x++)
        //{
        //    for (int y = 0; y < bounds.yMax; y++)
        //    {
        //        TileBase tile = blockTiles[x + y * bounds.size.x];
        //        if (tile != null)
        //        {
        //      //      Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
        //            MapSystem.Map1Server[x+1, y+1].isBlocked = true;
        //        }
        //        else
        //        {
        //          //  Debug.Log("x:" + x + " y:" + y + " tile: (null)");
        //        }
        //    }
        //}
    }
    private void SetLevel1TileInfo()
    {
        BoundsInt bounds;
        bounds = Level1.cellBounds;
        TileBase[] level1Tiles = Level1.GetTilesBlock(Level1.cellBounds);
        for (int x = 0; x < 200; x++)
        {
            for (int y = 0; y < bounds.yMax; y++)
            {
                TileBase tile = level1Tiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    //      Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    MapSystem.Map1Server[x + 1, y + 1].isBlocked = true;
                }
            }
        }

    }
    private void SetGroundTileInfo()
    {
        BoundsInt bounds;
        bounds = Ground.cellBounds;
        TileBase[] GroundTiles = Ground.GetTilesBlock(Ground.cellBounds);
        for (int x = 9; x < 200; x++)
        {
            for (int y = 2; y < 200; y++)
            {
                TileBase tile = GroundTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    if (tile.name == "Ground")
                    {
                        MapSystem.Map1Server[x - 9, y - 2].tileTypeId = 1;
                    }
                    if (tile.name == "Grass") MapSystem.Map1Server[x - 9, y - 2].tileTypeId = 0;
                }
            }
        }
    }



}
