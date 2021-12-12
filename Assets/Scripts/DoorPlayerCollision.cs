using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPlayerCollision : MonoBehaviour
{
    // Game's current level
    private string currentScene;

    // Level loader - includes fading transition
    [SerializeField]
    private LevelLoad levelLoad;

    // Audio manager to play sounds when door is locked/unlocked
    private AudioManagement audioManagement;
    void Start()
    {
        // Setting the current scene the player is on
        currentScene = SceneManager.GetActiveScene().name;
        // Finding the level's audio manager
        audioManagement = FindObjectOfType<AudioManagement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))

            // In 'main levels' of the game
            if (currentScene == "Level1" || currentScene == "Level2" || currentScene == "Level3")
            {
                if (PlayerPrefs.GetInt("Coins") >= 10)
                {
                    // Only allow the door to be opened if the player has 10 or more coins
                    StartCoroutine(NextLevel());
                }

                else
                {
                    audioManagement.Play("DoorLocked");
                    // Alert the player they need to pick up more coins to open the door
                    Debug.Log("You do not have enough coins to open the door. At least " +
                        "10 coins are required - you need to collect " + (10 - PlayerPrefs.GetInt("Coins")) + " more.");
                }
            }

            else
                // Door will automatically open in mini-game levels - there is no coin requirement
                StartCoroutine(NextLevel());


        IEnumerator NextLevel()
        {
            // Pause the game
            Time.timeScale = 0f;

            // Play the level complete sound
            audioManagement.Play("LevelComplete");

            // Wait for 0.9 seconds so the whole sound can play
            yield return new WaitForSecondsRealtime(0.9f);

            // Resume the game
            Time.timeScale = 1f;

            // Reset player checkpoint and coin collection status to prevent spawning issues
            PlayerPrefs.SetInt("Checkpoint", 0);
            PlayerPrefs.SetInt("Coins", 0);

            // Activate the level transition, and load the next level
            levelLoad.gameObject.SetActive(true);
            levelLoad.LoadNextLevel();
        }
    }
}