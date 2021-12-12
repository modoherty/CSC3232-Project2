using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPlayerCollision : MonoBehaviour
{
    private string currentScene;

    [SerializeField]
    private LevelLoad levelLoad;

    private AudioManagement audioManagement;
    void Start()
    {
        // Setting the current scene the player is on
        currentScene = SceneManager.GetActiveScene().name;
        audioManagement = FindObjectOfType<AudioManagement>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))

            // In 'main levels' of the game
            if (currentScene == "Level1" || currentScene == "Level2" || currentScene == "Level3")
            {
                if (PlayerPrefs.GetInt("Coins") >= 10)
                {
                    StartCoroutine(NextLevel());
                    // Only allow the door to be opened if the player has 10 or more coins
                    //levelLoad.gameObject.SetActive(true);
                    //levelLoad.LoadNextLevel();
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
                StartCoroutine(NextLevel());
                // Door will automatically open in mini-game levels - there is no coin requirement
                //levelLoad.gameObject.SetActive(true);
                //levelLoad.LoadNextLevel();

        IEnumerator NextLevel()
        {
            Time.timeScale = 0f;

            // Play the death sound
            audioManagement.Play("LevelComplete");

            // Wait for 0.7 seconds so the whole sound can play
            yield return new WaitForSecondsRealtime(0.9f);

            Time.timeScale = 1f;

            levelLoad.gameObject.SetActive(true);
            levelLoad.LoadNextLevel();
        }
    }
}
