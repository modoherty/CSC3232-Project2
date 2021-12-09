using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Range(0, 200), SerializeField]
    public int neighbourDist = 50;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, range * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 0.2f);
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
