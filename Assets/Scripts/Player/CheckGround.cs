using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public bool onTheGround;

    //
    void Awake()
    {
        onTheGround = false;
    }

    // Sent when ANOTHER object trigger collider enters a trigger collider attached to this object.
    void OnTriggerStay2D(Collider2D other)
    {
        //Compares the hitbox tag with its own tag.
        if(other.gameObject.CompareTag("Ground"))
        {
            onTheGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Compares the hitbox tag with its own tag.
        if(other.gameObject.CompareTag("Ground"))
        {
            onTheGround = false;
        }
    }
}
