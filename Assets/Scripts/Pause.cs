using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Boolean value determining whether the game is paused
    public static bool isPaused = false;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject quitPanel;

    [SerializeField]
    private GameObject levelUI;

    private AudioManagement audioManagement;

    // Start is called before the first frame update
    void Start()
    {
        // Finding the level's audio manager
        audioManagement = FindObjectOfType<AudioManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                // Resumes game
                GameResume();
            }
            else
            {
                // Pauses game
                GamePause();
            }
        }
    }
    public void GameResume()
    {
        audioManagement.gameObject.SetActive(true);
        audioManagement.Play("Click");
        // Deactivates pause menu
        pauseMenu.SetActive(false);
        // Reactivates level UI
        levelUI.SetActive(true);
        // Resumes time in the game
        Time.timeScale = 1f;
        // Sets paused bool value to false
        isPaused = false;
        // Replays the audio source
        
        audioManagement.Start();
    }

    private void GamePause()
    {
        // Activates pause menu
        pauseMenu.SetActive(true);
        // Deactivates level UI
        levelUI.SetActive(false);
        // Pauses time in the game
        Time.timeScale = 0f;
        // Sets paused bool value to true
        isPaused = true;
        // Pauses the audio currently playing in the game
        audioManagement.gameObject.SetActive(false);
    }
    public void RestartLevel()
    {
        /* Reset checkpoint and collected coin status - the player must start the level from the beginning 
           if they restart from the pause menu */
        
        bool checkpoint = false;
        PlayerPrefs.SetInt("Checkpoint", checkpoint ? 1 : 0);
        PlayerPrefs.SetInt("Coins", 0); 
        PlayerPrefs.Save();

        // Play clicking sound
        audioManagement.gameObject.SetActive(true);
        audioManagement.Play("Click");

        // Restarts the level and ensures that the game is unpaused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameResume();
    }

    public void OpenQuitMenu()
    {
        // Play clicking sound
        audioManagement.gameObject.SetActive(true);
        audioManagement.Play("Click");

        /* Opens the panel to ask the player for confirmation
           that they would like to quit the game */
        quitPanel.SetActive(true);
    }

    public void CloseQuitMenu()
    {
        // Play clicking sound
        audioManagement.gameObject.SetActive(true);
        audioManagement.Play("Click");

        // Closes the quit menu if the player decides not to quit the game
        quitPanel.SetActive(false);
    }

    public void QuitGame()
    {
        // Play clicking sound
        audioManagement.gameObject.SetActive(true);
        audioManagement.Play("Click");

        // Quits the game
        Debug.Log("Application has been quit.");
        Application.Quit();
    }
}
