using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Transform tf;
    private float speed;

    // Start is called before the first frame update
    void Awake()
    {
        tf = gameObject.GetComponent<Transform>();
        speed = 25f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tf.Rotate(new Vector3 (0f, 0f, speed) * Time.deltaTime);
    }
}
