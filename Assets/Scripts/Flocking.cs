using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* For this class, I followed a tutorial on YouTube by user
 * Holistic3D on how to set up flocking in 2D Unity games. 
 * The tutorial series I watched consisted of two videos:
 * 
 * https://www.youtube.com/watch?v=4mlyu9-WimM
 * https://www.youtube.com/watch?v=iFAyb6x-a3Q
 */
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
        goal = new Vector2(flockManager.transform.position.x - 6f, flockManager.transform.position.y + 4f);
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
    }

    Vector2 Seek(Vector2 target)
    {
        /* Returns the difference between the object's
           target position and its current location */
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

        Vector2 alignment = Alignment();
        Vector2 cohesion = Cohesion();

        // Sets the goal position
        Vector2 goalPosition;
        goalPosition = Seek(goal);

        /* Sets and normalises the current force, to ensure all objects
         * move with the same amount of force */
        currentForce = goalPosition + cohesion + alignment;
        currentForce = currentForce.normalized;

        ApplyForce(currentForce);
    }

    // This method will keep the shapes/objects together as cohesive units
    Vector2 Cohesion()
    {
        float neighbourDist = flockManager.GetComponent<FlockingGenerate>().neighbourDist;
        // Sum of the distances between each object
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach(GameObject shape in flockManager.GetComponent<FlockingGenerate>().shapes)
        {
            // Do nothing if on the current shape
            if (shape == this.gameObject)
                continue;

            float dist = Vector2.Distance(location, shape.GetComponent<Flocking>().location);
            // If the object is a neighbour of the other object
            if(dist < neighbourDist)
            {
                // Add their location to the sum
                sum += shape.GetComponent<Flocking>().location;
                count++;
            }
        }

        if(count > 0)
        {
            // Get the average distance between shapes
            sum /= count;
            return Seek(sum);
        }

        return Vector2.zero;
    }

    // This method will keep the shapes aligned together
    Vector2 Alignment()
    {
        float neighbourDist = flockManager.GetComponent<FlockingGenerate>().neighbourDist;
        // Sum of the distances between each object
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject shape in flockManager.GetComponent<FlockingGenerate>().shapes)
        {
            // Do nothing if on the current shape
            if (shape == this.gameObject)
                continue;

            float dist = Vector2.Distance(location, shape.GetComponent<Flocking>().location);
            // If the object is a neighbour of the other object
            if (dist < neighbourDist)
            {
                // Add their location to the sum
                sum += shape.GetComponent<Flocking>().location;
                count++;
            }
        }

        if (count > 0)
        {
            // Calculate the steer value to keep objects aligned
            Vector2 steer = sum - velocity;
            return steer;
        }

        return Vector2.zero;
    }

    void Update()
    {
        // Calls the flocking method
        Flock();
    }
}
