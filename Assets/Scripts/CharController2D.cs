using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharController2D : MonoBehaviour
{
    // NOTE - This movement code has been adapted from
    // CharacterRobotBoy in Unity's Standard Assets.

    private Transform player;
    private bool checkpoint;
    private string currentScene;

    [SerializeField] 
    // Character's movement speed
    private float speed = 15f;                    

    [SerializeField]
    // The force applied when the character jumps
    private float jumpForce = 500f;

    [SerializeField]
    // Determines what the 'ground' in the level comprises of
    private LayerMask whatIsGround;

    // Checks if the player is on the ground, and the size of the radius of the circle when the check is
    // performed
    private Transform groundCheck;
    float groundCheckRadius = 0.75f;

    // Bool determining if the player is on the ground
    private bool grounded;  
    
    // References the animator, Rigidbody2D and SpriteRenderer components for the character
    private Animator animator;            
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private AudioManagement audioManagement;

    // Checks if the player is facing to the right - always true at the beginning of the level
    private bool faceRight = true;

    // References to the player health and lives scripts - used when the player takes damage/loses a life
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private PlayerLives playerLives;

    [SerializeField]
    private LevelLoad levelLoad;

    private void Start()
    {
        // The transform component of the player character
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // The current scene the player is on
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            // Determines if the player has triggered the checkpoint in the first level
            checkpoint = PlayerPrefs.GetInt("Checkpoint") == 1 ? true : false;

            if (checkpoint)
                // Sets the player's starting position to the checkpoint if they triggered it
                player.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        }

        levelLoad.gameObject.SetActive(false);
    }

    private void Awake()
    {
        // Getting each component of the player character object
        groundCheck = transform.Find("GroundCheck");
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioManagement = FindObjectOfType<AudioManagement>();
    }


    private void FixedUpdate()
    {
        grounded = false;

        // Checks that the character is positioned on the ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        // Updates the animator, to allow the transition to jumping state if applicable
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("vSpeed", rigidBody.velocity.y);

        if(rigidBody.position.y < -12)
        {
            audioManagement.Play("Death");
            playerLives.LoseLife(1);
        }

        // This represents a 20% reduction in the scale of gravity on the main character, as well as a 20% reduction in their mass.
        // I included this to allow higher, further jumps in the higher sections of the levels.
        if (rigidBody.transform.position.y > 12)
        {
            rigidBody.gravityScale = 4.0f;
            rigidBody.mass = 0.64f;
        }

        // Returns the character's mass and gravity scale back to normal when they are below the low-gravity area
        else
        {
            rigidBody.gravityScale = 4.8f;
            rigidBody.mass = 0.8f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player is colliding with an obstacle in the level
        if(collision.gameObject.CompareTag("Mace") || collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Saw"))
        {
            // Adds force to the mace to prevent it from losing all momentum
            collision.rigidbody.AddForce(new Vector2(20.0f, 20.0f), ForceMode2D.Impulse);

            // Adds impulse to the player to push them away from the object they are collding with
            //collision.otherRigidbody.AddForce(new Vector2(collision.otherRigidbody.velocity.x / 4, collision.otherRigidbody.velocity.y / 4), ForceMode2D.Impulse);
            animator.SetBool("isHit", true);

            audioManagement.Play("Hurt1");
            // Take 1 damage
            playerHealth.TakeDamage(1);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mace") || collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Saw") || collision.gameObject.CompareTag("Fire"))
        {
            animator.SetBool("isHit", false);
            // Adds impulse to the player character to push them away from the obstacle
            collision.otherRigidbody.AddForce(new Vector2(-20.0f, -20.0f), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            // Saves the player's position where they 'collided' with the checkpoint
            checkpoint = true;
            PlayerPrefs.SetFloat("PlayerX", player.position.x);
            PlayerPrefs.SetFloat("PlayerY", player.position.y);
            PlayerPrefs.SetFloat("PlayerZ", player.position.z);

            // Stores whether the player has triggered the checkpoint, so they can start from it if they lose a life
            PlayerPrefs.SetInt("Checkpoint", checkpoint ? 1 : 0);
            PlayerPrefs.Save();
        }

        if (collision.gameObject.CompareTag("Apple"))
        {
            // Gain 1 health if the player picks up an apple
            playerHealth.TakeDamage(-1);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Bananas"))
        {
            // Gain 1 life if the player picks up a banana
            playerLives.GainLife(1);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Watermelon"))
        {
            // Gain 2 health if the player picks up a watermelon
            playerHealth.TakeDamage(-2);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            // Stores the amount of coins the player has collected throughout the level
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
            PlayerPrefs.Save();
            audioManagement.Play("CollectCoin");
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Fire"))
        {
            // Take 2 damage if the player touches fire
            animator.SetBool("isHit", true);
            audioManagement.Play("Hurt2");
            playerHealth.TakeDamage(2);
        }
    }


    public void Move(float move, bool jump)
    {
        if (grounded)
        {
            // Set speed animator parameter to absolute value of the horizontal input
            animator.SetFloat("Speed", Mathf.Abs(move));

            // Move the character by setting its velocity
            rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);

            // Logic to make the character face left when moving to the left,
            // and right when moving to the right
            if (move < 0 && faceRight)
            {
                // Flips the character sprite to face left
                sprite.flipX = true;
                faceRight = false;
            }
            if(move > 0 && !faceRight)
            {
                // Flips the sprite back to face to the right
                sprite.flipX = false;
                faceRight = true;
            }
        }

        // If the player presses the jump button while the character is on the ground
        if (grounded && jump && animator.GetBool("Grounded"))
        {
            // Set animator's grounded property to false to play jump animation
            grounded = false;
            animator.SetBool("Grounded", false);

            // Resets the character's velocity before jumping to prevent 'infinite jumps'
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            // Applying the jump force to the rigid body, allowing it to jump
            rigidBody.AddForce(Vector2.up * jumpForce);
        }
    }
}
