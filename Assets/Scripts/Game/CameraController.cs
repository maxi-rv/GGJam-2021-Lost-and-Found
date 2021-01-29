using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<Transform> Players;
    public Vector3 offset;
    private float speed;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Awake()
    {
        speed = 0.1f;
        offset = new Vector3(0f, 0f, 0f);
    }

    // FixedUpdate is called multiple times per frame.
    void FixedUpdate()
    {
        var boundsMove = new Bounds(Players[0].position, new Vector3(1f, 1f, 1f));

        for (int i=0; i<Players.Count; i++)
        {
            boundsMove.Encapsulate(Players[i].position);
        }

        Vector3 centerPosition = boundsMove.center;
        Vector3 desiredPosition = centerPosition+offset;
        Vector3 fixedPosition =  Vector3.zero;

        if(desiredPosition.y < 0f)
            fixedPosition = new Vector3(0f,0f,desiredPosition.z);
        else
            fixedPosition = new Vector3(0f,desiredPosition.y,desiredPosition.z);

        //Moves the camera
        transform.position = Vector3.SmoothDamp(transform.position, fixedPosition, ref velocity, speed);
    }
}