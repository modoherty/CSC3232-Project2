using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Platform components
    private Animator animator;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        // Initialising the animator and Rigidbody components 
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Platforms will fall downwards, except in the higher, lower-gravity areas
            // of the level, where they will rise upwards
            if (rb2D.position.y < 10)
            {
                // Applies force to make the platform fall
                Vector3 fall = new Vector3(0.0f, 15f, 0.0f);
                rb2D.AddForce(fall * rb2D.velocity.y);
            }

            else
            {
                // Applies force in the opposite direction - platform 'falls upwards'
                Vector3 fall = new Vector3(0.0f, -15f, 0.0f);
                rb2D.AddForce(fall * (rb2D.velocity.y * -1));
            }

            if (rb2D.position.y < -30 || rb2D.position.y > 30)
                // 'Destroy's platform if it passes the bounds of the level in either direction
                rb2D.gameObject.SetActive(false);

            // Sets the animator to play the platform moving animation
            animator.SetBool("isColliding", true);
        }
    }
}
