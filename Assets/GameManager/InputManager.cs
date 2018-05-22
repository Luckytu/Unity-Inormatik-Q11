using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PathFinder pathFinder;
    
    private TileSelect targetTile;
    private UnitMovement markedUnit;
    private UnitMovement targetUnit;

    private bool unitSelected = false;
    private bool cardSelected = false;
    private bool pathResetable = false;
    private bool pathSelected = false;
    private bool firstTileSelected = false;

    // Use this for initialization
    void Start ()
    {
        pathFinder = GetComponent<PathFinder>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pathResetable && Input.GetMouseButtonDown(0))
        {
            pathFinder.resetPath();
            unitSelected = false;
        }

        if (pathSelected)
        {
            pathResetable = true;
        }

        if (Input.GetMouseButtonDown(1) && !markedUnit.isStillMoving())
        {
            StartCoroutine(markedUnit.moveUnit());
        }
        
        if(unitSelected)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                cardSelected = true;
                //unitcontroller soll karte 1 einsetzen
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                cardSelected = true;
                //unitcontroller soll karte 2 einsetzen
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                cardSelected = true;
                //unitcontroller soll karte 3 einsetzen
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                cardSelected = true;
                //unitcontroller soll karte 4 einsetzen
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                cardSelected = true;
                //unitcontroller soll karte 5 einsetzen
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                cardSelected = true;
                //unitcontroller soll karte 6 einsetzen
            }
        }
    }

    public void setTargetTile(TileSelect targetTile) { this.targetTile = targetTile; }
    public TileSelect getTargetTile() { return targetTile; }

    public void markUnit(UnitMovement markedUnit) { this.markedUnit = markedUnit; }
    public UnitMovement getMarkedUnit() { return markedUnit; }

    public void setUnitSelected(bool unitSelected) { this.unitSelected = unitSelected; }
    public void setPathResetable(bool pathResetable) { this.pathResetable = pathResetable; }
    public void setPathSelected(bool pathSelected) { this.pathSelected = pathSelected; }
    public void setFirstTileSelected(bool firstTileSelected) { this.firstTileSelected = firstTileSelected; }

    public bool isUnitSelected() { return unitSelected; }
    public bool isPathResetable() { return pathResetable; }
    public bool isPathSelected() { return pathSelected; }
    public bool isFirstTileSelected() { return firstTileSelected; }
}