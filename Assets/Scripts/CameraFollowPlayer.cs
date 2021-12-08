using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    private Transform player;

    float offsetX = 12.14f;

    public bool boundaries;

    [SerializeField]
    private Vector3 minPosition; // =new Vector3(-4, 0, -10);
    [SerializeField]
    private Vector3 maxPosition; // =new Vector3(1000, 100, -10);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void LateUpdate()
    {
        /* Vector3 v = transform.position;

         v.x = player.position.x;
         v.y = player.position.y;

         v.x += offsetX;
         v.y += offsetY;

         transform.position = v;*/

        Vector3 v = transform.position;

        v.x = player.position.x;
        if (player.position.y > 0 && player.position.y < 20)
            v.y = player.position.y;
        else
            v.y = transform.position.y;

        v.x += offsetX;

        transform.position = v;

        if (boundaries)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y),
                Mathf.Clamp(transform.position.z, minPosition.z, maxPosition.z));

        }
    }
}
