using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHitBoxConroller : MonoBehaviour {

    private Quaternion rotation;
    
    private BoxCollider boxCollider;
    private UnitController unitController;

    void Awake()
    {
        rotation = transform.rotation;

        boxCollider = GetComponent<BoxCollider>();
        unitController = GetComponentInParent<UnitController>();
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
        unitController.markThisUnit();
        unitController.findPath();
    }
}
