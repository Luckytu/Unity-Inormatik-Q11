using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float timeToMove;

    private GameObject[] tiles;
    public TileSelect currentTile;

    private PathFinder pathFinder;

    private int x;
    private int z;

    private bool stillMoving;

	// Use this for initialization
	void Start ()
    {
        tiles = GameObject.Find("GameManager").GetComponent<GameManagerBase>().getTiles();
        pathFinder = GameObject.Find("GameManager").GetComponent<PathFinder>();

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
                currentTile = tiles[i].GetComponent<TileSelect>();
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

    public void findPath()
    {
        if (!pathFinder.isPathSelected())
        {
            pathFinder.findPath(currentTile.getTileID());
            pathFinder.setPathSelected(true);
        }
    }

    private void moveToTile(TileSelect nextTile) 
    {
        stillMoving = true;
        float timePassed = 0;

        Vector3 currentPos = currentTile.transform.position;
        Vector3 nextPos = nextTile.transform.position;

        Vector3 movement = nextPos - currentPos;
        movement.y = 0;
        movement *= Time.deltaTime;

        transform.Translate(movement);
    }
}
