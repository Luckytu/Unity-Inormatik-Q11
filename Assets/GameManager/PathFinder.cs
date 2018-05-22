using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class PathFinder : GameManagerBase
{
    private List<TilePathFinder> priorityList;
    private List<TileSelect> pathList;
    private Stack<TileSelect> pathStack;

    private InputManager inputManager;

    private void Start()
    {
        getDataFromBase();

        pathList = new List<TileSelect>();

        inputManager = GetComponent<InputManager>();
    }

    public void findPath(int startID, float maxHeight)
    {
        int currentMinValue;

        prepareList(startID);
        
        while(priorityList.Count > 0)
        {
            currentMinValue = findMinValue();
            
            if (priorityList[currentMinValue].getValue() != int.MaxValue)
            {
                priorityList[currentMinValue].updateAdjacentTiles(maxHeight);

                priorityList.RemoveAt(currentMinValue);
            }
            else
            {
                break;
            }
        }
    }

    public void prepareList(int startID)
    {
        priorityList = new List<TilePathFinder>(tilePathFinder);
        
        for (int i = 0; i < priorityList.Count; i ++)
        {
            priorityList[i].setValue(int.MaxValue);
            priorityList[i].setPreviousTile(null);
        }

        priorityList[startID].setValue(0);
    }

    public void prepareList()
    {
        priorityList = new List<TilePathFinder>(tilePathFinder);

        for (int i = 0; i < priorityList.Count; i++)
        {
            priorityList[i].setValue(int.MaxValue);
            priorityList[i].setPreviousTile(null);
        }
    }

    private int findMinValue()
    {
        int min = int.MaxValue;
        int minPos = 0;

        for(int i = 0; i < priorityList.Count; i++)
        {
            if(priorityList[i].getValue() < min)
            {
                min = priorityList[i].getValue();
                minPos = i;
            }
        }

        return minPos;
    }

    public void unMarkPath()
    {
        GameObject[] pathLights = GameObject.FindGameObjectsWithTag("TileLightSelect");
        for (int i = 0; i < pathLights.Length; i++)
        {
            GameObject.Destroy(pathLights[i]);
        }
    }

    public void resetPath()
    {
        unMarkPath();
        prepareList();
        clearPath();

        inputManager.setPathSelected(false);
        inputManager.setPathResetable(false);
        inputManager.setFirstTileSelected(false);
    }

    public void addTileToPath(TileSelect tile)
    {
        pathList.Add(tile);

        if (!inputManager.isFirstTileSelected())
        {
            inputManager.setTargetTile(tile);
            inputManager.setFirstTileSelected(true);
            inputManager.getTargetTile().setTileOccupied(true);
        }
    }

    public Stack<TileSelect> getPath()
    {
        pathList.RemoveAt(pathList.Count - 1);

        pathStack = new Stack<TileSelect>(pathList);

        return pathStack;
    }

    public void clearPath() { pathList.Clear(); }
}
