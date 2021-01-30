using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteGenerator : MonoBehaviour
{
    //Game Objects
    [SerializeField] private GameObject stalactitePrefab;

    //Variables
    public float stalactiteSpeed;
    public float iHaveAStalactite;

    // Start is called before the first frame update
    void Awake()
    {
        stalactiteSpeed = 12f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rcHit2D = Physics2D.Raycast(gameObject.transform.position, Vector3.down);

        if(rcHit2D)
        {
            if(rcHit2D.transform.gameObject.CompareTag("Player"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
        rotation.eulerAngles = new Vector3(0f, 0f, 0f);
        
        GameObject stalactite = Instantiate(stalactitePrefab, gameObject.transform.position, rotation);
        
        Rigidbody2D stalactiteRB = stalactite.GetComponent<Rigidbody2D>();

        Vector2 direction = Vector2.down;
        
        stalactiteRB.AddForce(direction*stalactiteSpeed, ForceMode2D.Impulse);
    }
}
