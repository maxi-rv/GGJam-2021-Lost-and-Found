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

    
    void Update()
    {
        //Despues de cierto tiempo, la flecha se elimina.
        //Invoke("DestroyItself", 3f);
    }
        
    //Sent when ANOTHER object trigger collider enters a trigger collider attached to this object.
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void DestroyItself()
    {
        Destroy(gameObject);
    }
}