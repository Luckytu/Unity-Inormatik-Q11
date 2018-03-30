using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    private Transform tileTransform;

    private float height;

    private int x;
    private int z;
    private int tileID;

    private GameObject[] adjacentTileList;
    
    public GameObject lightSource;
    public GameObject lightBox;
    public float lightDistance;

	// Use this for initialization
	void Awake ()
    {
        tileTransform = gameObject.GetComponent<Transform>();
	}
    
    public void setHeight(float newHeight)
    {
        tileTransform.localScale = new Vector3(1, newHeight, 1);
        tileTransform.position += new Vector3(0, (newHeight / 2 - 0.5f), 0);
        height = newHeight;
    }

    public void setTileID(int tileID)
    {
        this.tileID = tileID;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public void setZ(int z)
    {
        this.z = z;
    }

    public float getHeight()
    {
        return height;
    }

    public void setList(GameObject[] newList)
    {
        for(int i = 0; i < 4; i++)
        {
            adjacentTileList[i] = newList[i];
        }
    }

    private void OnMouseEnter()
    {
        Quaternion newQuaternion = new Quaternion();
        newQuaternion.eulerAngles = new Vector3(90, 0, 0);

        GameObject.Instantiate(lightSource, tileTransform.position + new Vector3(0, height / 2 + lightDistance, 0), newQuaternion);

        GameObject.Instantiate(lightBox, tileTransform.position + new Vector3(0, height / 2 + 1, 0), new Quaternion());
    }

    private void OnMouseExit()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("TileLight"));
        GameObject.Destroy(GameObject.FindGameObjectWithTag("TileLightLimit"));
    }
}
