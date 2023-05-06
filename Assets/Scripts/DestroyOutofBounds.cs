using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutofBounds : MonoBehaviour
{
    //Created variables
    private float zDestroy = 25;
    private float xDestroy = 25;
    private float yDestroy = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy object if moves outside of game room
        if (transform.position.z > zDestroy || transform.position.z < -zDestroy)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -xDestroy || transform.position.x > xDestroy)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < yDestroy || transform.position.y > 25)
        {
            Destroy(gameObject);
        }
    }
}
