using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpriteController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.localPosition = new Vector3(0, GetComponent<SpriteRenderer>().bounds.size.y / 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
