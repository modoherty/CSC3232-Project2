using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPlayerCollision : MonoBehaviour
{
    private string currentScene;
    // Start is called before the first frame update

    [SerializeField]
    private LevelLoad levelLoad;
    void Start()
    {
        // Setting the current scene the player is on
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))

            // In 'main levels' of the game
            if (currentScene == "Level1" || currentScene == "Level2")
            {
                if (PlayerPrefs.GetInt("Coins") >= 10)
                {
                    // Only allow the door to be opened if the player has 10 or more coins
                    levelLoad.gameObject.SetActive(true);
                    levelLoad.LoadNextLevel();
                }

                else
                    // Alert the player they need to pick up more coins to open the door
                    Debug.Log("You do not have enough coins to open the door. At least " +
                        "10 coins are required - you need to collect " + (10 - PlayerPrefs.GetInt("Coins")) + " more.");
            }

            else
                // Door will automatically open in mini-game levels - there is no coin requirement
                levelLoad.gameObject.SetActive(true);
                levelLoad.LoadNextLevel();
    }
}
