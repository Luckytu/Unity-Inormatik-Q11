using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHitBoxConroller : MonoBehaviour {

    private Quaternion rotation;
    
    private BoxCollider boxCollider;
    private UnitMovement unitMovement;

    void Awake()
    {
        rotation = transform.rotation;

        boxCollider = GetComponent<BoxCollider>();
        unitMovement = GetComponentInParent<UnitMovement>();
    }

    void Update()
    {
        transform.rotation = rotation;
    }

    public void setHitBox(float hitBoxHeight)
    {
        boxCollider.size = new Vector3(0.8f, hitBoxHeight, 0.8f);
        boxCollider.center = new Vector3(0, hitBoxHeight / 2, 0);
    }

    private void OnMouseDown()
    {
        unitMovement.markThisUnit();
        unitMovement.findPath();
    }
}
