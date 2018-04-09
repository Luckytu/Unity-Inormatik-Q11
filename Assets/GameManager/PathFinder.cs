using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class PathFinder : GameManagerBase
{
    private List<TilePathFinder> priorityList;

    private void Start()
    {
        getDataFromBase();

        findPath(0);
    }

    public void findPath(int startID)
    {
        int previousListCount = 0;
        int currentMinValue;

        prepareList(startID);

        while(priorityList.Count > 0)
        {
            currentMinValue = findMinValue();
            
            if ((previousListCount != priorityList.Count) && (priorityList[currentMinValue].hasNeighboursAndValueNotMax()))
            {
                previousListCount = priorityList.Count;

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
}
