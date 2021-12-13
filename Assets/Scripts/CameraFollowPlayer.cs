using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    /* Variables for the player, the camera's boundaries and 
       minimum/maximum positions */
    private Transform player;

    float offsetX = 12.14f;

    public bool boundaries;

    [SerializeField]
    private Vector3 minPosition;
    [SerializeField]
    private Vector3 maxPosition;

    void Start()
    {
        // Gets the player character
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void LateUpdate()
    {
        Vector3 v = transform.position;

        // Updates the camera's y position if the player is positioned beneath 20 on the y axis
        v.x = player.position.x;
        if (player.position.y > 0 && player.position.y < 20)
            v.y = player.position.y;
        else
            v.y = transform.position.y;

        // Adds the offset to the x position
        v.x += offsetX;

        // Updates the camera's position
        transform.position = v;

        if (boundaries)
        {
            // Clamps the camera so it does not move past the level's set boundaries
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y),
                Mathf.Clamp(transform.position.z, minPosition.z, maxPosition.z));

        }
    }
}
