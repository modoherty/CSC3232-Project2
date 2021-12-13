using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupChest : MonoBehaviour
{
    private Animator animator;
    private AudioManagement audioManagement;

    private GameObject[] chests;

    [SerializeField]
    private PlayerLives playerLives;


    void Start()
    {
        // Initialising the necessary components
        animator = GetComponent<Animator>();
        audioManagement = FindObjectOfType<AudioManagement>();

        chests = GameObject.FindGameObjectsWithTag("Chest");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("Open", true);
            // Plays a sound when the chest opens
            audioManagement.Play("Bonus");

            /* This is a random number generator used to determine what reward is inside each chest.
            *  The player is more likely to only gain 1 life after opening their chosen chest, compared to
            *  the bigger prizes - gaining 3 or 5 lives. */

            int random = Random.Range(0, 10);

            // 10% chance to gain 5 lives
            if(random >= 0 && random < 1)
            {
                playerLives.GainLife(5);
                Debug.Log("You gained 5 lives!");
            }

            // 60% chance to gain 1 life
            if (random > 1 && random < 7)
            {
                playerLives.GainLife(1);
                Debug.Log("You gained 1 life!");
            }

            // 30% chance to gain 3 lives
            if (random > 7 && random <= 10)
            {
                playerLives.GainLife(3);
                Debug.Log("You gained 3 lives!");
            }

            foreach(GameObject chest in chests)
                // Destroy all remaining chests
                GameObject.Destroy(chest);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       animator.SetBool("Open", false);
    }
}
