using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    public int highScore;
    public string highScoreName;
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(268 - 1.5f + step * x, 150 + 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        highScoreText.text = "Best score: " + MainHandler.Instance.playerHighScoreName + ": " + MainHandler.Instance.playerHighScore;

        if (MainHandler.Instance.playerHighScoreName == "")
        {
            MainHandler.Instance.playerHighScoreName = "nobody";
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            SetHighScore();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        scoreText.text = "Score : " + m_Points;
        MainHandler.Instance.playerScore = m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void SetHighScore()
    {
        if (m_Points > MainHandler.Instance.playerHighScore)
        {
            highScore = m_Points;
            highScoreName = MainHandler.Instance.playerName;

            if (MainHandler.Instance.playerName != MainHandler.Instance.playerHighScoreName)
                MainHandler.Instance.playerHighScoreName = MainHandler.Instance.playerName;

            if (MainHandler.Instance.playerScore >= MainHandler.Instance.playerHighScore)
                MainHandler.Instance.playerHighScore = highScore;

            highScoreText.text = "Best score: " + MainHandler.Instance.playerHighScoreName + ": " + MainHandler.Instance.playerHighScore;

        }
    }
}
