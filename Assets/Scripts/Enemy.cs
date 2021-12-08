using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/* Script to setup AI Pathfinding in 2D.
 * I followed a tutorial on Unity 2D AI
 * by user Etredal on YouTube for this class.
 * 
 * Link to video: https://www.youtube.com/watch?v=sWqRfygpl4I
 */


public class Enemy : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform enemyTarget;
    public float activationDistance = 20f;
    public float pathUpdateTime = 0.4f;

    [Header("Physics")]
    public float movementSpeed = 20f;
    public float nextWaypointDistance = 2f;
    public float jumpNodeHeight = 0.5f;
    public float jumpForce = 0.9f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Enemy Behaviour")]
    public bool followEnabled = true;
    public bool canJump = true;
    public bool directionLook = true;

    private Path path;
    private int waypoint = 0;
    bool isGrounded = false;
    SpriteRenderer sprite;
    Seeker seeker;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        InvokeRepeating("PathUpdate", 0f, pathUpdateTime);
    }

    private void FixedUpdate()
    {
        if(TargetInDistance() && followEnabled)
        {
            FollowPath();
        }
    }

    private void PathUpdate()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb2d.position, enemyTarget.position, OnPathComplete);
        }
    }

    private void FollowPath()
    {
        if (path == null)
            return;

        if (waypoint >= path.vectorPath.Count)
            return;

        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up,
            GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

        Vector2 dir = ((Vector2)path.vectorPath[waypoint] - rb2d.position).normalized;
        Vector2 force = dir * movementSpeed * Time.deltaTime;

        if(canJump && isGrounded)
        {
            if(dir.y > jumpNodeHeight)
                rb2d.AddForce(Vector2.up * movementSpeed * jumpForce);
        }

        rb2d.AddForce(force);

        float nextWaypoint = Vector2.Distance(rb2d.position, path.vectorPath[waypoint]);
        if (nextWaypoint < nextWaypointDistance)
            waypoint++;

        if (directionLook)
        {
            if (rb2d.velocity.x > 0.05f)
                sprite.flipX = false;
            else if (rb2d.velocity.x < -0.05f)
                sprite.flipX = true;
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, enemyTarget.transform.position) < activationDistance;
    }

    private void OnPathComplete(Path path1)
    {
        if(!path1.error)
        {
            path = path1;
            waypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
