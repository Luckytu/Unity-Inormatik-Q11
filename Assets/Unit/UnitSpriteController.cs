using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpriteController : MonoBehaviour {

    public Sprite sprite;

    public float spriteHeight;

	// Use this for initialization
	void Start ()
    {
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        transform.localPosition = new Vector3(0, spriteHeight / 2, 0);

        setSprite();

        transform.parent.gameObject.GetComponentInChildren<UnitHitBoxConroller>().setHitBox(spriteHeight);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setSprite()
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public float getSpriteHeight() { return spriteHeight; }
}
