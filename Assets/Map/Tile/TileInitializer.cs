using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInitializer : TileBase
{
    

    // Use this for initialization
    private void Awake()
    {
        tileTransform = GetComponent<Transform>();
        tileInitializer = GetComponent<TileInitializer>();

        x = (int)tileTransform.position.x;
        z = (int)tileTransform.position.z;
    }

    public void setHeight(float height)
    {
        tileTransform.localScale = new Vector3(1, height, 1);
        tileTransform.position += new Vector3(0, (height / 2), 0);
        this.height = height;
    }

    public void initializeTileID(int tileID)
    {
        this.tileID = tileID;
    }

    public void setAdjacentTiles(ref GameObject tile0)
    {
        adjacentTiles = new GameObject[1];
        adjacentTiles[0] = tile0;

        adjacentTilePathFinder = new TilePathFinder[1];
        adjacentTilePathFinder[0] = adjacentTiles[0].GetComponent<TilePathFinder>();

        setDataInBase();
    }

    public void setAdjacentTiles(ref GameObject tile0, ref GameObject tile1)
    {
        adjacentTiles = new GameObject[2];
        adjacentTiles[0] = tile0;
        adjacentTiles[1] = tile1;

        adjacentTilePathFinder = new TilePathFinder[2];
        adjacentTilePathFinder[0] = adjacentTiles[0].GetComponent<TilePathFinder>();
        adjacentTilePathFinder[1] = adjacentTiles[1].GetComponent<TilePathFinder>();

        setDataInBase();
    }

    public void setAdjacentTiles(ref GameObject tile0, ref GameObject tile1, ref GameObject tile2)
    {
        adjacentTiles = new GameObject[3];
        adjacentTiles[0] = tile0;
        adjacentTiles[1] = tile1;
        adjacentTiles[2] = tile2;

        adjacentTilePathFinder = new TilePathFinder[3];
        adjacentTilePathFinder[0] = adjacentTiles[0].GetComponent<TilePathFinder>();
        adjacentTilePathFinder[1] = adjacentTiles[1].GetComponent<TilePathFinder>();
        adjacentTilePathFinder[2] = adjacentTiles[2].GetComponent<TilePathFinder>();

        setDataInBase();
    }

    public void setAdjacentTiles(ref GameObject tile0, ref GameObject tile1, ref GameObject tile2, ref GameObject tile3)
    {
        adjacentTiles = new GameObject[4];
        adjacentTiles[0] = tile0;
        adjacentTiles[1] = tile1;
        adjacentTiles[2] = tile2;
        adjacentTiles[3] = tile3;

        adjacentTilePathFinder = new TilePathFinder[4];
        adjacentTilePathFinder[0] = adjacentTiles[0].GetComponent<TilePathFinder>();
        adjacentTilePathFinder[1] = adjacentTiles[1].GetComponent<TilePathFinder>();
        adjacentTilePathFinder[2] = adjacentTiles[2].GetComponent<TilePathFinder>();
        adjacentTilePathFinder[3] = adjacentTiles[3].GetComponent<TilePathFinder>();

        setDataInBase();
    }

    public int getX() { return x; }
    public int getZ() { return z; }
}