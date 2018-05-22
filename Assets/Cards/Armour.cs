using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : Card {

    private int healthPointsToAdd = 12;

    private void Start()
    {
        active = false;
        passive = true;
    }

    public override void activeBehaviour()
    {
        throw new System.NotImplementedException();
    }

    public override void passiveBehaviour()
    {
        unit.addHealthPoints(healthPointsToAdd);
    }
}
