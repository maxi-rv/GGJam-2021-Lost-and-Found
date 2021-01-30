using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour
{
    public bool againstWallLeft;
    public bool againstWallRight;
    [HideInInspector] public CheckWallAux checkRight;
    [HideInInspector] public CheckWallAux checkLeft;

    //
    void Awake()
    {
        // Gets CHILDREN
        GameObject RightCheck = gameObject.transform.Find("RightCheck").gameObject;
        GameObject LeftCheck = gameObject.transform.Find("LeftCheck").gameObject;

        // Gets COMPONENTS from the Children
        checkRight = RightCheck.GetComponent<CheckWallAux>();
        checkLeft = LeftCheck.GetComponent<CheckWallAux>();
        
        againstWallLeft = false;
        againstWallRight = false;
    }

    void FixedUpdate() 
    {
        againstWallLeft = checkLeft.againstWall;
        againstWallRight = checkRight.againstWall;
    }
}
