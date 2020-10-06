using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] GameObject _popUp;

    int _currentScore;
    bool _menuOpen = false;

    void Start()
    {
        //CursorLock();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_menuOpen == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _popUp.SetActive(true);
                _menuOpen = true;
                CursorUnlock();
                Time.timeScale = 0f;
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeLevel();
            }
        }
    }

    public void ExitLevel()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New High Score: " + _currentScore);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeLevel()
    {
        _popUp.SetActive(false);
        _menuOpen = false;
        CursorLock();
        Time.timeScale = 1f;
    }

    public void IncreaseScore(int scoreIncrease)
    {
        _currentScore += scoreIncrease;
        _currentScoreTextView.text = "Current Score: " + _currentScore.ToString();
    }

    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
