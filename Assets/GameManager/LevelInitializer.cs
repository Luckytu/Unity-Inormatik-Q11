using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class LevelInitializer : GameManagerBase
{
    private float[,] mapHeights;
    private float[] mapHeights1DCleaned;
    private float[] mapHeights1D;

    public GameObject originalTile;

    [Range(5, 50)]
    public int maxX = 10;

    [Range(5, 50)]
    public int maxY = 10;

    private void Awake()
    {
        mapHeightsInitialize();
        setTilesData();

        setDataInBase();
    }

    protected void mapHeightsInitialize()
    {
        mapHeights1D = new float[maxX * maxY];

        mapHeights = new float[maxX, maxY];

        for (int i = 0; i < maxX; i++)
        {
            for (int k = 0; k < maxY; k++)
            {
                mapHeights[i,k] = Random.Range(0, 5);

                if (mapHeights[i, k] > 0)
                {
                    GameObject.Instantiate(originalTile, new Vector3((float)i, 0, (float)k), new Quaternion());
                }
                mapHeights1D[maxX * i + k] = mapHeights[i, k];
            }
        }

        tiles = GameObject.FindGameObjectsWithTag("Tile");
        tileInitializer = new TileInitializer[tiles.Length];
        tilePathFinder = new TilePathFinder[tiles.Length];

        mapHeights1DCleaned = mapHeights1D.Where(val => val != 0).ToArray();
    }

    protected void setTilesData()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tileInitializer[i] = tiles[i].GetComponent<TileInitializer>();
            tilePathFinder[i] = tiles[i].GetComponent<TilePathFinder>();
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            tileInitializer[i].setHeight(mapHeights1DCleaned[i]);
            tileInitializer[i].initializeTileID(i);

            List<GameObject> tempAdjacentTiles = new List<GameObject>();

            for (int k = 0; k < tiles.Length; k++)
            {
                if ((tileInitializer[k].getX() - 1 == tileInitializer[i].getX()) && (tileInitializer[k].getZ() == tileInitializer[i].getZ()))
                {
                    tempAdjacentTiles.Add(tiles[k]);
                }
                else
                {
                    if ((tileInitializer[k].getX() + 1 == tileInitializer[i].getX()) && (tileInitializer[k].getZ() == tileInitializer[i].getZ()))
                    {
                        tempAdjacentTiles.Add(tiles[k]);
                    }
                    else
                    {
                        if ((tileInitializer[k].getZ() - 1 == tileInitializer[i].getZ()) && (tileInitializer[k].getX() == tileInitializer[i].getX()))
                        {
                            tempAdjacentTiles.Add(tiles[k]);
                        }
                        else
                        {
                            if ((tileInitializer[k].getZ() + 1 == tileInitializer[i].getZ()) && (tileInitializer[k].getX() == tileInitializer[i].getX()))
                            {
                                tempAdjacentTiles.Add(tiles[k]);
                            }
                        }
                    }
                }
            }

            GameObject[] tempAdjacentTilesArray = tempAdjacentTiles.ToArray();

            switch (tempAdjacentTilesArray.Length)
            {
                case 1:
                    tileInitializer[i].setAdjacentTiles(ref tempAdjacentTilesArray[0]);
                    break;
                case 2:
                    tileInitializer[i].setAdjacentTiles(ref tempAdjacentTilesArray[0], ref tempAdjacentTilesArray[1]);
                    break;
                case 3:
                    tileInitializer[i].setAdjacentTiles(ref tempAdjacentTilesArray[0], ref tempAdjacentTilesArray[1], ref tempAdjacentTilesArray[2]);
                    break;
                case 4:
                    tileInitializer[i].setAdjacentTiles(ref tempAdjacentTilesArray[0], ref tempAdjacentTilesArray[1], ref tempAdjacentTilesArray[2], ref tempAdjacentTilesArray[3]);
                    break;
                default:
                    break;
            }
        }
    }
}
