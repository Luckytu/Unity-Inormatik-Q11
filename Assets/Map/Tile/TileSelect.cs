using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : TileBase {
    
    private TilePathFinder tilePathFinder;
    private PathFinder pathFinder;
    private InputManager inputManager;

    public GameObject lightSelectSource;
    public GameObject lightPathSource;
    public float lightDistance;

    private bool tileOccupied;

    // Use this for initialization
    void Start ()
    {
        getDataFromBase();

        tilePathFinder = GetComponent<TilePathFinder>();
        pathFinder = GameObject.Find("GameManager").GetComponent<PathFinder>();
        inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();

        height = tileTransform.localScale.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    private void markTileOnPath()
    {
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.eulerAngles = new Vector3(90, 0, 0);

        UnitMovement markedUnit = inputManager.getMarkedUnit();
        
        if(tilePathFinder.getValue() < markedUnit.GetComponent<UnitController>().getActionPoints())
        {
            GameObject.Instantiate(lightPathSource, tileTransform.position + new Vector3(0, height / 2 + lightDistance, 0), newQuaternion);
        }

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
        
        if(inputManager.getMarkedUnit() != null)
        {
            inputManager.getMarkedUnit().setTargetTile(this);
        }

        GameObject.Instantiate(lightSelectSource, tileTransform.position + new Vector3(0, height / 2 + lightDistance, 0), newQuaternion);

        if(tilePathFinder.getPreviousTile() != null && !tileOccupied)
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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            tileOccupied = true;
        }
    }

    public void setTileOccupied(bool tileOccupied) { this.tileOccupied = tileOccupied; }
    public bool isTileOccupied() { return tileOccupied; }
}
