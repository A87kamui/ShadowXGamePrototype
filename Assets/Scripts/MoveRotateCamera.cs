using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRotateCamera : MonoBehaviour
{
    //Variables access to components
    public GameObject player;

    //Created variables
    public float distanceFromPlayer;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Main Camera")
        {
            offset = new Vector3(0f, 02f, 0f);
            distanceFromPlayer = 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Sets position at direction and set distance
        transform.position = player.transform.position - player.transform.forward * distanceFromPlayer;

        //Rotates object so the forward vector points at arrgument
        transform.LookAt(player.transform.position);

        //Set postion = current position + Vector3 offset
        transform.position = transform.position + offset;
        //Update rotation of object
        transform.rotation *= Quaternion.Euler(17, 0, 0);
    }
}
