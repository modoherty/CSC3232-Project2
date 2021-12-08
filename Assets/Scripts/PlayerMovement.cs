using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    private CharController2D character;
    private bool jump;


    private void Awake()
    {
        // Getting the player character from the character controller script
        character = GetComponent<CharController2D>();
    }


    private void Update()
    {
        if (!jump)
        {
            /* The input for jumping is read in the Update function
             * in order to avoid missing inputs, and make jumping more responsive */
            jump = Input.GetKeyDown(KeyCode.Space);
        }
    }


    private void FixedUpdate()
    {
        // Get the player's input to make the player move left or right, using the arrow keys 
        // or WASD keys
        float h = Input.GetAxis("Horizontal");
        // Pass all parameters to the character control script, so the player will move
        character.Move(h, jump);
        jump = false;
    }
}
