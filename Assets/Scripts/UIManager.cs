using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    //We can create a separate script for accessing the Objects under UI Canvas, but it is not suggested. Calling/accessing them from
    //the script of Canvas is best way to do it because those Objects come under Canvas and there is a built in library to access them
    //easily from the script of UI Canvas.

    [SerializeField]
    private Text _scoreText; //For accessing the Text Object under the UI Canvas. 

    private int _score;

    [SerializeField]
    private Text _bestScoreText;

    int _bestScore;

    [SerializeField]
    private Image _remainingLivesDisplay; //For accessing the Image Object under the UI Canvas.

    [SerializeField]
    private Text _gameOverText;

    //[SerializeField]
    //private Text _restartText;

    [SerializeField]
    private Text _startWaveText;

    [SerializeField]
    private Button _restartButton;

    [SerializeField]
    private Sprite[] _livesSprites; //To store the lives sprites and input them one by one to the Image Object to Display remaining lives.

    private GameManager _gameManager;

    [SerializeField]
    private Button _pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.enabled = false; //_gameOverText.gameObject.SetActive(false); --> this is also correct
        //_restartButton.enabled = false;
        _restartButton.gameObject.SetActive(false);
        //_restartText.gameObject.SetActive(false); //_restartTest.enabled = false; --> this is also correct
        _startWaveText.enabled = false;
        _pauseButton.gameObject.SetActive(true);

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //reference for GameManager.
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        BestScoreSystem();
    }

    public void ScoreSystem(int points)
    {
        _score = _score + points;
        _scoreText.text = "Score: " + _score;
    }

    public void BestScoreSystem()
    {
        _bestScoreText.text = "Best: " + _bestScore;
    }

    public void GetBestScoreRecord(int _highScoreRecord)
    {
        _bestScore = _highScoreRecord;
    }

    public void SetBestScoreRecord()
    {
        if (_bestScore < _score)
        {
            _bestScore = _score;
            _gameManager.OnGameOver(_bestScore);
        }
    }

    public void LivesSystem(int lives)
    {
        _remainingLivesDisplay.sprite = _livesSprites[lives];
    }

    public void GameOverSequence()
    {
        _gameOverText.enabled = true; //_gameOverText.gameObject.SetActive(true); --> this is also correct
        //_restartButton.enabled = true;
        _restartButton.gameObject.SetActive(true);
        //_restartText.gameObject.SetActive(true); // _restart.Text.enabled = true; --> this is also correct
        _gameManager.RestartConfirmation();
        _pauseButton.gameObject.SetActive(false);
    }

    public void StartWaveTextEnabled()
    {
        _startWaveText.enabled = true;
    }

    public void StartWaveTextDisabled()
    {
        _startWaveText.enabled = false;
    }
}
