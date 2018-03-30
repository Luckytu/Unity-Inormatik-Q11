using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class LevelInitialize : MonoBehaviour {

    private float[,] mapHeights;

    public GameObject originalTile;
    private GameObject[] tileClones;
    
    private int maxX = 10;
    private int maxY = 10;

	void Start ()
    {
        tileClones = new GameObject[maxX * maxY];

        mapHeightsInitialize();
    }

    private void mapHeightsInitialize()
    {
        float[] mapHeights1D = new float[maxX * maxY];

        mapHeights = new float[10, 10]
        {
            {1, 2, 3, 4, 5, 4, 3, 2, 1, 2},
            {1.1f, 2, 3, 4, 4.5f, 4, 3, 2, 1, 2.5f},
            {1.2f, 2, 3, 4, 4, 4, 3, 2, 1, 3},
            {1.3f, 2, 3, 4, 3, 4, 3, 2, 1, 3.5f},
            {1.4f, 2, 3, 4, 1, 4, 3, 2, 1, 4},
            {1.5f, 2, 3, 4, 3, 4, 3, 2, 1, 4.5f},
            {1.6f, 2, 3, 4, 4, 4, 3, 2, 1, 5},
            {1.7f, 2, 3, 4, 4.5f, 4, 3, 2, 1, 5.5f},
            {1.8f, 2, 3, 4, 5, 4, 3, 2, 1, 6},
            {1.9f, 2, 3, 4, 5, 4, 3, 2, 1, 6.5f},
        };

        for(int i = 0; i < maxX; i++)
        {
            for(int k = 0; k < maxY; k++)
            {
                if(mapHeights[i, k] > 0)
                {
                    GameObject.Instantiate(originalTile, new Vector3((float)i, 0, (float)k), new Quaternion());
                }
                mapHeights1D[maxX * i + k] = mapHeights[i, k];
            }
        }

        tileClones = GameObject.FindGameObjectsWithTag("Tile");
        
        mapHeights1D = mapHeights1D.Where(val => val != 0).ToArray();

        for (int i = 0; i < tileClones.Length; i++)
        {
            tileClones[i].GetComponent<TileController>().setHeight(mapHeights1D[i]);
        }
    }
}