using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/* Script to setup AI Pathfinding in 2D.
 * I followed a tutorial on Unity 2D AI
 * by user Etredal on YouTube for this class.
 * 
 * Link to video: https://www.youtube.com/watch?v=sWqRfygpl4I
 * 
 * NOTE - I had to import the A* Pathfinding Project by Aron Granberg,
 * since Unity in 2D does not natively support NavMeshes and 
 * platformer-style pathfinding.
 * 
 * Link to the A* Project: https://arongranberg.com/astar/download
 */


public class Enemy : MonoBehaviour
{
    /* This is used to set up the enemy pathfinding.
     * Here, the enemy's 'target' is added (which they should follow),
     * the pathfinding activation distance (which is how far away the target
     * can be before being seen by the enemy), and the path update time, 
     * which is how often the pathfinding algorithm will update.
     */
    [Header("Pathfinding")]
    public Transform enemyTarget;
    public float activationDistance = 20f;
    public float pathUpdateTime = 0.4f;

    /* Physics variables - which allow the movement speed of the enemy, its
     * next waypoint distance, and control of their jump distances. 
     */
    [Header("Physics")]
    public float movementSpeed = 20f;
    public float nextWaypointDistance = 2f;
    public float jumpNodeHeight = 0.5f;
    public float jumpForce = 0.9f;
    public float jumpCheckOffset = 0.1f;

    /* Custom behaviour, which can be edited for different types of 
     * enemy. These determine whether they can follow the target,
     * jump when the target jumps (or to reach other platforms),
     * and look for the target in both directions.
     */
    [Header("Custom Enemy Behaviour")]
    public bool followEnabled = true;
    public bool canJump = true;
    public bool directionLook = true;

    // Setting up pathfinding variables
    private Path path;
    private int waypoint = 0;
    bool isGrounded = false;

    /* Variables related to the specific enemy - its sprite,
       attached Seeker script, and its Rigidbody2D component */
    SpriteRenderer sprite;
    Seeker seeker;
    Rigidbody2D rb2d;

    void Start()
    {
        // Setting up the components
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        /* Run the PathUpdate method every pathUpdateTime seconds, i.e. update the path
           using the specified path update time */
        InvokeRepeating("PathUpdate", 0f, pathUpdateTime);
    }

    private void FixedUpdate()
    {
        if(TargetInDistance() && followEnabled)
            // Follow the path to the target if it is within the specified distance
            FollowPath();
    }

    private void PathUpdate()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
            // Create the best path from the enemy to the target
            seeker.StartPath(rb2d.position, enemyTarget.position, OnPathComplete);
    }

    private void FollowPath()
    {
        // Return if there is no path to the target
        if (path == null)
            return;

        // Return if the nearest path waypoint is too far away
        if (waypoint >= path.vectorPath.Count)
            return;

        /* Perform a raycast on the enemy object, and the ground's colliders, to check whether
           the enemy is currently on the ground */
        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up,
            GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

        // Vectors for the enemy's current direction, and the force to be applied to make it move
        Vector2 dir = ((Vector2)path.vectorPath[waypoint] - rb2d.position).normalized;
        Vector2 force = dir * movementSpeed * Time.deltaTime;

        if(canJump && isGrounded)
        {
            if(dir.y > jumpNodeHeight)
                /* Add force to the enemy to make it jump, based on the specified 
                   movement speed and jumping force */
                rb2d.AddForce(Vector2.up * movementSpeed * jumpForce);
        }

        // Add force to the rigidbody, whether it is able to jump or not
        rb2d.AddForce(force);

        // Get the next waypoint for the enemy to move to 
        float nextWaypoint = Vector2.Distance(rb2d.position, path.vectorPath[waypoint]);
        if (nextWaypoint < nextWaypointDistance)
            waypoint++;

        // If it is able to look in both directions for the target
        if (directionLook)
        {
            // If the enemy is moving right
            if (rb2d.velocity.x > 0.05f)
                // Keeps the sprite facing right
                sprite.flipX = false;
            // If the enemy is moving left
            else if (rb2d.velocity.x < -0.05f)
                // Flips the sprite so it is facing to the left
                sprite.flipX = true;
        }
    }

    private bool TargetInDistance()
    {
        // Finds out if the enemy's 'target' is within its activation distance - i.e. it can see the target
        return Vector2.Distance(transform.position, enemyTarget.transform.position) < activationDistance;
    }

    private void OnPathComplete(Path path1)
    {
        if(!path1.error)
        {
            // Resets the next waypoint if the enemy's path is completed
            path = path1;
            waypoint = 0;
        }
    }
}
