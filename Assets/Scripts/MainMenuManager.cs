using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Debug.Log("Loading Game...");
        SceneManager.LoadScene(1); //Load Game scene...
        Debug.Log("Game Loaded Successfully.");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    public void Reset_Best_Score()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            Debug.Log("Reseting HighScore record...");
            PlayerPrefs.DeleteKey("HighScore");
            Debug.Log("HighScore record reset complete.");
        }
        else
        {
            //Do Nothing...
            Debug.Log("HighScore record already reset.");
        }
    }
}
