using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Transform cameraTransform;
    private Vector3 movement;
    private GameObject cameraObject;
    public Camera cameraProperties;

    public float fieldOfView;
    public float maxFieldOfView;
    public float minFieldOfView;

    public float height;
    public float speed;

    private Vector3 rotationAxis;

    public int rotationSteps;
    public float rotationAngleX;
    public float rotationAngleYStart;
    private float rotationAngleY;

    public float rotationDistance;
    public float rotationDuration;

    private bool rotating = false;

    // Use this for initialization
    void Start()
    {
        cameraProperties.orthographic = true;
        cameraProperties.orthographicSize = fieldOfView;

        cameraTransform = GetComponent<Transform>();
        cameraTransform.position = new Vector3(0, 0, 0);
        cameraTransform.eulerAngles = new Vector3(0, rotationAngleYStart, 0);

        cameraObject = GameObject.Find("Main Camera");
        cameraObject.transform.localPosition = new Vector3(0, height, -rotationDistance);
        cameraObject.transform.localEulerAngles = new Vector3(rotationAngleX, 0, 0);

        rotationAngleY = 360 / (float)rotationSteps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -speed;
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                movement.x = speed;
            }
            else
            {
                movement.x = 0;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.z = -speed;
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                movement.z = speed;
            }
            else
            {
                movement.z = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            rotateInitialize(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotateInitialize(false);
        }

        cameraTransform.Translate(movement * Time.deltaTime);

        if (Input.GetAxis("Mouse ScrollWheel") != 0f && cameraProperties.orthographicSize >= minFieldOfView && cameraProperties.orthographicSize <= maxFieldOfView)
        {
            cameraProperties.orthographicSize -= Input.GetAxis("Mouse ScrollWheel");
        }

        if(cameraProperties.orthographicSize < minFieldOfView)
        {
            cameraProperties.orthographicSize = minFieldOfView;
        }

        if (cameraProperties.orthographicSize > maxFieldOfView)
        {
            cameraProperties.orthographicSize = maxFieldOfView;
        }
    }

    private void rotateInitialize(bool clockWise)
    {
        Quaternion start = cameraTransform.rotation;
        Quaternion end = clockWise? Quaternion.Euler(0, start.eulerAngles.y + rotationAngleY, 0) : Quaternion.Euler(0, start.eulerAngles.y - rotationAngleY, 0);
        
        if(!rotating)
        {
            rotating = true;
            StartCoroutine(rotate(start, end));
        }
    }

    private IEnumerator rotate(Quaternion start, Quaternion end)
    {
        float timePassed = 0;

        while(timePassed <= rotationDuration)
        {
            cameraTransform.rotation = Quaternion.Slerp(start, end, timePassed / rotationDuration);            
            yield return null;
            timePassed += Time.deltaTime;
        }

        rotating = false;
        cameraTransform.rotation = end;
    }
}