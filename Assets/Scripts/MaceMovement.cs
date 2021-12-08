using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private HingeJoint2D hinge;
    // Start is called before the first frame update
    void Start()
    {
        // Setting up the rigidbody and hinge joint
        rigidBody = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Applies the force to the mace, making it rotate like a pendulum
        if(hinge.jointAngle == 0)
            rigidBody.AddForce(new Vector3(400.0f, 400.0f, 0.0f), ForceMode2D.Impulse);

        if (rigidBody.transform.position.y > 12)
        {
            /* Applies 20% reduction in gravity scale and mass in the
             * 'low-gravity' areas of the level */
            rigidBody.gravityScale = 4.0f;
            rigidBody.mass = 32;
        }
    }
}
