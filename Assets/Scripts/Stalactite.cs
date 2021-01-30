using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    private Collider2D hitBox;

    void Awake()
    {
        // CHILDREN
        GameObject HitBox = gameObject.transform.Find("HitBox").gameObject;

        // COMPONENT
        hitBox = HitBox.GetComponent<Collider2D>();
    }

    
    void Start()
    {
        //Despues de cierto tiempo, la flecha se elimina.
        Invoke("DestroyItself", 6f);
    }

    private void DestroyItself()
    {
        Destroy(gameObject);
    }
}