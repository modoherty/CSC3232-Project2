using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private GameObject player;

    void Start()
    {
        // Initialising saw and player components
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
            if (player.transform.position.x + 20 >= rigidBody.transform.position.x)
                // Wakes up saw so it will only start moving if the player is within 20 units - it can "see" them
                rigidBody.WakeUp();

            if (rigidBody.IsAwake())
            {
                
                if (rigidBody.angularVelocity >= 0)
                // Initially move the saw to the left - towards the player 
                    rigidBody.AddForce(new Vector3(-60.0f, -60.0f, 0.0f), ForceMode2D.Force);

                else
                // If the saw hits a wall, it should move to the right 
                rigidBody.AddForce(new Vector3(60.0f, 60.0f, 0.0f), ForceMode2D.Force);


                if (rigidBody.position.y < -30)
                    // 'Destroy' saw if it falls below the bounds of the level
                    rigidBody.gameObject.SetActive(false);
        }
    }
}
