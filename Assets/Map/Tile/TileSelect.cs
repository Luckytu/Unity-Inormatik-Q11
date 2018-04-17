﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : TileBase {
    
    private TilePathFinder tilePathFinder;
    private PathFinder pathFinder;

    public GameObject lightSelectSource;
    public GameObject lightPathSource;
    public float lightDistance;

    // Use this for initialization
    void Start ()
    {
        getDataFromBase();

        tilePathFinder = GetComponent<TilePathFinder>();
        pathFinder = GameObject.Find("GameManager").GetComponent<PathFinder>();

        height = tileTransform.localScale.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1) && !pathFinder.getMarkedUnit().isStillMoving())
        {
            StartCoroutine(pathFinder.getMarkedUnit().moveUnit());
        }
    }


    private void markTileOnPath()
    {
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.eulerAngles = new Vector3(90, 0, 0);

        GameObject.Instantiate(lightPathSource, tileTransform.position + new Vector3(0, height / 2 + lightDistance, 0), newQuaternion);

        if(tilePathFinder.getPreviousTile() != null)
        {
            tilePathFinder.getPreviousTile().GetComponent<TileSelect>().markTileOnPath();
        }
    }

    private void addTileToPath()
    {
        pathFinder.addTileToPath(this);

        if (tilePathFinder.getPreviousTile() != null)
        {
            tilePathFinder.getPreviousTile().GetComponent<TileSelect>().addTileToPath();
        }
    }

    private void OnMouseEnter()
    {
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.eulerAngles = new Vector3(90, 0, 0);

        GameObject.Instantiate(lightSelectSource, tileTransform.position + new Vector3(0, height / 2 + lightDistance, 0), newQuaternion);

        if(tilePathFinder.getPreviousTile() != null)
        {
            markTileOnPath();
            addTileToPath();
        }
    }

    private void OnMouseExit()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("TileLight"));

        pathFinder.unMarkPath();
        pathFinder.clearPath();
    }


}
