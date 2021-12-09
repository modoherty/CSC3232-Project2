using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocking : MonoBehaviour
{
    /* Fields to set up flocking - the manager or flocking area,
       shape/object's location and velocity */
    [SerializeField]
    public GameObject flockManager;

    [SerializeField]
    private Vector2 location;

    [SerializeField]
    private Vector2 velocity;

    // Goal position for the shape and the force to be applied
    Vector2 goal;
    Vector2 currentForce;


    // Start is called before the first frame update
    void Start()
    {
        goal = flockManager.transform.position;
        //velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        //location = new Vector2(this.gameObject.transform.position.x, 
                               //this.gameObject.transform.position.y);
    }

    Vector2 Seek(Vector2 target)
    {
        return (target - location);
    }

    void ApplyForce(Vector2 force)
    {
        // Applies force to the object's Rigidbody
        Vector3 f = new Vector3(force.x, force.y, 0);
        this.GetComponent<Rigidbody2D>().AddForce(f);
    }

    void Flock()
    {
        // Gets the velocity and location of each shape
        velocity = this.GetComponent<Rigidbody2D>().velocity;
        location = this.transform.position;

        // Sets the goal position
        Vector2 goalPosition;
        goalPosition = Seek(goal);

        /* Sets and normalises the current force, to ensure all objects
         * move with the same amount of force */
        currentForce = goalPosition;
        currentForce = currentForce.normalized;

        ApplyForce(currentForce);
    }



    // Update is called once per frame
    void Update()
    {
        // Calls the flocking method
        Flock();
    }
}
