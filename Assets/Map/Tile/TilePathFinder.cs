using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePathFinder : TileBase {

    private GameObject previousTile;
    private int value;

    public int APCostFlat;

    // Use this for initialization
    void Start ()
    {
        getDataFromBase();
	}

    public int getAPCost(int adjacentTileID)
    {
        float heightDifference = height - adjacentTilePathFinder[globalToLocalID(adjacentTileID)].height;

        if (heightDifference == 0)
        {
            return APCostFlat;
        }
        else
        {
            if (heightDifference > 0)
            {
                return (int)(heightDifference * 3);
            }
            else
            {
                return (int)(heightDifference * -2);
            }
        }
    }

    private int globalToLocalID(int globalID)
    {
        for (int i = 0; i < adjacentTilePathFinder.Length; i++)
        {
            if (adjacentTilePathFinder[i].getTileID() == globalID)
            {
                return i;
            }
        }

        return 0;
    }

    public void updateAdjacentTiles()
    {
        if (adjacentTilePathFinder != null)
        {
            for (int i = 0; i < adjacentTilePathFinder.Length; i++)
            {
                if (adjacentTilePathFinder[i].getValue() > value + adjacentTilePathFinder[i].getAPCost(tileID))
                {
                    adjacentTilePathFinder[i].setValue(value + adjacentTilePathFinder[i].getAPCost(tileID));
                    adjacentTilePathFinder[i].setPreviousTile(gameObject);
                }
            }
        }
    }

    public bool hasNeighboursAndValueNotMax()
    {
        return ((adjacentTiles != null) && (value != int.MaxValue));
    }

    public GameObject getPreviousTile() { return previousTile; }
    public int getValue() { return value; }

    public void setPreviousTile(GameObject previousTile) { this.previousTile = previousTile; }
    public void setValue(int value) { this.value = value; }
}
