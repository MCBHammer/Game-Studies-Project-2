using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Text _highScoreTextView;

    // Start is called before the first frame update
    void Start()
    {
        int _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = _highScore.ToString();
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        int _highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = _highScore.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
