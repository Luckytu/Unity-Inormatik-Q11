using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class PathFinder : GameManagerBase
{
    private List<TilePathFinder> priorityList;
    private Stack<TileSelect> path;

    private bool pathUnMarkAble = false;
    private bool pathSelected = false;

    private void Start()
    {
        getDataFromBase();

        findPath(0);

        path = new Stack<TileSelect>();
    }

    private void Update()
    {
        if(pathUnMarkAble && Input.GetMouseButtonDown(0))
        {
            unMarkPath();
            prepareList();
            clearPath();

            pathSelected = false;
            pathUnMarkAble = false;
        }

        if(pathSelected)
        {
            pathUnMarkAble = true;
        }
    }

    public void findPath(int startID)
    {
        int currentMinValue;

        prepareList(startID);

        while(priorityList.Count > 0)
        {
            currentMinValue = findMinValue();
            
            if (priorityList[currentMinValue].getValue() != int.MaxValue)
            {
                priorityList[currentMinValue].updateAdjacentTiles();

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

    public void addTileToPath(TileSelect tile) { path.Push(tile); }
    public Stack<TileSelect> getPath() { return path; }
    public void clearPath() { path.Clear(); }

    public bool isPathSelected() { return pathSelected; }
    public void setPathSelected(bool pathSelected) { this.pathSelected = pathSelected; }
}
