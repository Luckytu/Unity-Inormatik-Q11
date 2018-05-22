using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class UnitMovement : MonoBehaviour
{
    public float timeToMove;
    private bool stillMoving;

    private UnitController unitController;

    private GameObject[] tiles;
    public TileSelect currentTile;
    public TileSelect nextTile;
    public TileSelect targetTile;

    private PathFinder pathFinder;
    private InputManager inputManager;

    private int x;
    private int z;

    public int unitID;

	// Use this for initialization
	void Start ()
    {
        tiles = GameObject.Find("GameManager").GetComponent<GameManagerBase>().getTiles();
        pathFinder = GameObject.Find("GameManager").GetComponent<PathFinder>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
        unitController = GetComponent<UnitController>();

        findCurrentPosition();
        findCurrentTile();
        setUnitHeight();

        currentTile.setTileOccupied(true);
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
        if (!inputManager.isPathSelected())
        {
            pathFinder.findPath(currentTile.getTileID(), GetComponent<UnitController>().getMaxHeight());
            inputManager.setPathSelected(true);
        }
    }

    public void markThisUnit()
    {
        pathFinder.resetPath();
        inputManager.setUnitSelected(true);
        inputManager.markUnit(this);
    }

    public IEnumerator moveUnit()
    {
        stillMoving = true;
        Stack <TileSelect> path = new Stack <TileSelect> (pathFinder.getPath().Reverse());

        pathFinder.resetPath();

        for (int i = path.Count; i > 0; i--)
        {
            int apCost = currentTile.GetComponent<TilePathFinder>().getAPCost(path.Peek().getTileID(), unitController.getMaxHeight());

            if (apCost < unitController.getActionPoints())
            {
                unitController.updateActionPoints(-apCost);

                nextTile = path.Pop();

                yield return StartCoroutine(moveToNextTile(nextTile));
            }
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

            if (timePassed >= timeToMove / 2)
            {
                newPos.y = nextTile.getHeight();
            }

            transform.position = newPos;

            yield return null;

            timePassed += Time.deltaTime;
        }

        nextPos.y = nextTile.getHeight();
        transform.position = nextPos;

        currentTile.setTileOccupied(false);
        nextTile.setTileOccupied(true);

        currentTile = nextTile;
    }

    public void setTargetTile(TileSelect targetTile) { this.targetTile = targetTile; }
    public TileSelect getTargetTile() { return targetTile; }

    public bool isStillMoving() { return stillMoving; }

    public TileSelect getNextTile() { return nextTile; }
    public TileSelect getCurrentTile() { return currentTile; }
}
