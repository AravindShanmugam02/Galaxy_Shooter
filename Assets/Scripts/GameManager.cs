using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    private GameObject _gamePausePanel;

    private Animator _gamePausePanelAnimator;

    private UIManager _ui;

    private int _highScore;

    // Start is called before the first frame update
    void Start()
    {
        _ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_ui == null)
        {
            Debug.LogError("_ui is empty in GameManager");
        }
        else
        {
            int _highScoreRecord = PlayerPrefs.GetInt("HighScore", 0);
            _ui.GetBestScoreRecord(_highScoreRecord);
        }

        _gamePausePanel = GameObject.Find("Game_Pause_Panel");
        if (_gamePausePanel == null)
        {
            Debug.LogError("_gamePausePanel is empty in GameManager");
        }
        else
        {
            _gamePausePanel.SetActive(false);
        }

        _gamePausePanelAnimator = _gamePausePanel.GetComponent<Animator>();
        if (_gamePausePanelAnimator == null)
        {
            Debug.LogError("_gamePausePanelAnimator is empty in GameManager");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        //if(_isGameOver == true && Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(0); //SceneManager.LoadScene("Main"); -->This method is also correct, But it is not efficient. Using integer is efficient.
        //}

    }

    public void RestartButtonClick()
    {
        if(_isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RestartConfirmation()
    {
        _isGameOver = true;
    }

    public void OnGameOver(int bestScoreRecord)
    {
        _highScore = bestScoreRecord;
        PlayerPrefs.SetInt("HighScore", _highScore);
    }

    public void OnPauseEnabled()
    {
        _gamePausePanel.SetActive(true);
        _gamePausePanelAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _gamePausePanelAnimator.SetBool("isPause",true);
        Time.timeScale = 0f;
    }

    public void OnPauseDisabled()
    {
        _gamePausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
