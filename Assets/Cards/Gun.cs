using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Card
{
    private InputManager inputManager;

    private float range = 10f;
    private int damage = 6;

    private void Start()
    {
        active = true;
        passive = false;

        //inputManager = GameObject.Find("GameManger").GetComponent<InputManager>();
    }

    public override void activeBehaviour()
    {
        Vector3 origin = unit.GetComponentInChildren<BoxCollider>().transform.position;
        Vector3 hit;
    }

    public override void passiveBehaviour()
    {
        throw new System.NotImplementedException();
    }
}