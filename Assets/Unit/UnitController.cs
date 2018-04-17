using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class UnitController : MonoBehaviour
{
    public float timeToMove;
    private bool stillMoving;

    private GameObject[] tiles;
    public TileSelect currentTile;

    private PathFinder pathFinder;

    private int x;
    private int z;

    public int unitID;

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

    public void markThisUnit()
    {
        pathFinder.markUnit(this);
    }

    public IEnumerator moveUnit()
    {
        stillMoving = true;
        Stack <TileSelect> path = new Stack <TileSelect> (pathFinder.getPath().Reverse());

        pathFinder.resetPath();

        for (int i = path.Count; i > 0; i--)
        {
            yield return StartCoroutine(moveToNextTile(path.Pop()));
        }

        stillMoving = false;
    }

    private IEnumerator moveToNextTile(TileSelect nextTile)
    {
        float timePassed = 0;

        Vector3 currentPos = currentTile.transform.position;
        currentPos.y = currentTile.getHeight();

        Vector3 nextPos = nextTile.transform.position;
        nextPos.y = currentTile.getHeight();

        Vector3 pathToNext = nextPos - currentPos;

        while (timePassed <= timeToMove)
        {
            Vector3 newPos = currentPos + pathToNext * (timePassed / timeToMove);

            if(timePassed >= timeToMove / 2)
            {
                newPos.y = nextTile.getHeight();
            }

            transform.position = newPos;

            yield return null;

            timePassed += Time.deltaTime;
        }

        nextPos.y = nextTile.getHeight();
        transform.position = nextPos;
        
        currentTile = nextTile;
    }

    public bool isStillMoving() { return stillMoving; }
}
