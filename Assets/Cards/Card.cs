using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    protected UnitController unit;

    protected int actionPointCost;
    protected int cardCost;
    protected int coolDown;

    protected bool active;
    protected bool passive;

    public abstract void activeBehaviour();
    public abstract void passiveBehaviour();

    public void setUnit(UnitController unit)
    {
        this.unit = unit;
    }

    public bool isPassive() { return passive; }
    public bool isActive() { return active; }
}
