using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /* Code for the main menu - used 
     * to start the game, open the controls menu and 
     * quit the game. 
     */

    // Game objects for the start menu, controls menu, and quit menu
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject controlsMenu;

    [SerializeField]
    private GameObject quitPanel;

    public void BeginGame()
    {
        // Loads the next scene - which is the first level of the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenControlsMenu()
    {
        // Opens the controls menu, which displays the game controls
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void CloseControlsMenu()
    {
        // Closes the controls menu and returns to the main menu
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenQuitMenu()
    {
        /* Opens the panel to ask the player for confirmation
           that they would like to quit the game */
        quitPanel.SetActive(true);
    }

    public void CloseQuitMenu()
    {
        // Closes the quit menu if the player decides not to quit the game
        quitPanel.SetActive(false);
    }

    public void QuitGame()
    {
        // Quits the game
        Debug.Log("Application has been quit.");
        Application.Quit();
    }
}
