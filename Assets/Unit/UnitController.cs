using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private GameObject[] tiles;
    public GameObject currentTile;

    private int x;
    private int z;

	// Use this for initialization
	void Start ()
    {
        tiles = GameObject.Find("GameManager").GetComponent<GameManagerBase>().getTiles();

        findCurrentPosition();
        findCurrentTile();
        setUnitHeight();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = GameObject.Find("Main Camera").transform.rotation;
    }

    private void findCurrentTile()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if((x == tiles[i].GetComponent<TileBase>().getX()) && (z == tiles[i].GetComponent<TileBase>().getZ()))
            {
                currentTile = tiles[i];
                break;
            }
        }
    }

    private void findCurrentPosition()
    {
        x = (int)transform.position.x;
        z = (int)transform.position.z;
    }

    private void setUnitHeight()
    {
        transform.position = new Vector3(transform.position.x, currentTile.GetComponent<TileBase>().getHeight(), transform.position.z);
    }
}
