using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnInfo
{
    private int startTile;
    private int team;

    public UnitSpawnInfo(int startTile, int team)
    {
        this.startTile = startTile;
        this.team = team;
    }

    public int getTeam() { return team; }
    public int getStartTile() { return startTile; }
}
