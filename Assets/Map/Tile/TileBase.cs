using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBase : MonoBehaviour {

    protected TileBase tileBase;

    protected Transform tileTransform;
    protected TileInitializer tileInitializer;

    protected GameObject[] adjacentTiles;
    protected TilePathFinder[] adjacentTilePathFinder;

    protected int x;
    protected int z;
    protected int tileID;
    protected float height;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void setDataInBase()
    {
        tileBase = GetComponent<TileBase>();

        tileBase.tileTransform = GetComponent<Transform>();

        tileBase.x = (int)tileTransform.position.x;
        tileBase.z = (int)tileTransform.position.z;

        setAdjacentTiles();
        setAdjacentTilePathFinder();
        setTileID();
        setHeight();
    }

    protected void getDataFromBase()
    {
        tileBase = GetComponent<TileBase>();

        tileTransform = GetComponent<Transform>();
        tileID = tileBase.getTileID();
        height = tileBase.getHeight();
        x = (int)tileTransform.position.x;
        z = (int)tileTransform.position.z;

        adjacentTiles = tileBase.getAdjacentTiles();
        adjacentTilePathFinder = tileBase.getAdjacentTilePathFinder();
    }

    protected void setAdjacentTiles() { tileBase.adjacentTiles = adjacentTiles; }
    protected void setAdjacentTilePathFinder() { tileBase.adjacentTilePathFinder = adjacentTilePathFinder; }
    protected void setTileID() { tileBase.tileID = tileID; }
    protected void setHeight() { tileBase.height = height; }

    protected GameObject[] getAdjacentTiles() { return adjacentTiles; }
    protected TilePathFinder[] getAdjacentTilePathFinder() { return adjacentTilePathFinder; }
    protected int getTileID() { return tileID; }

    public float getHeight() { return height; }
    public int getX() { return x; }
    public int getZ() { return z; }
}
