using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private bool[] teamsAlive;
    private int teamCount;
    private List<UnitController> units;

	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < teamsAlive.Length; i++)
        {
            teamsAlive[i] = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startNextTurn()
    {

    }

    public void addUnit(UnitController unit) { units.Add(unit); } 
    public void killUnit(int unitID)
    {
        for(int i = 0; i < units.Count; i++)
        {
            if(units[i].getUnitID() == i)
            {
                units.RemoveAt(i);
                checkIfTeamIsDead(i);
            }
        }
    }

    private void checkIfTeamIsDead(int teamID)
    {

    }
}