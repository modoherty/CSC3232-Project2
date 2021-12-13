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
public class FlockingGenerate : MonoBehaviour
{
    /* Fields for the prefab to be used to generate the array of shapes,
       how many shapes to create, and the range they can appear within the level */
    [SerializeField]
    public GameObject[] shapes;

    [SerializeField]
    private GameObject shapePrefab;

    [SerializeField]
    private int numberOfShapes;

    [SerializeField]
    private Vector2 range = new Vector2(10, 10);

    [Range(0.0f, 5.0f), SerializeField]
    public float neighbourDist;

    void Start()
    {
        // Create a new array of GameObjects
        shapes = new GameObject[numberOfShapes];

        for(int i = 0; i < numberOfShapes; i++)
        {
            // The position of the object on the screen - within the boundaries of the specified range
            Vector3 shapePosition = new Vector3(Random.Range(-range.x, range.x),
                                                Random.Range(-range.y, range.y), 0);

            // Instantiate the prefab on the screen and in the level
            shapes[i] = Instantiate(shapePrefab, this.transform.position + shapePosition, 
                                    Quaternion.identity) as GameObject;
            shapes[i].GetComponent<Flocking>().flockManager = this.gameObject;
        }
    }
}
