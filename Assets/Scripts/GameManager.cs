using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MainManager;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    
    private bool m_Started = false;
    private int m_Points;
    

    
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
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        List<PlayerScore> scores =  MainManager.Instance.scores;
        if(scores == null || scores.Count == 0)
        {
            BestScoreText.text = "Best Score : " + MainManager.Instance.name + " : 0";
        }
        else
        {
            PlayerScore best = new PlayerScore();
            bool debut = true;
            foreach(PlayerScore score in scores)
            {
                if (debut)
                {
                    best = score;
                    debut = false;
                }
                else
                {
                    if (score.score >= best.score) best = score;
                }
            }
            BestScoreText.text = "Best Score : " + best.name + " : " + best.score.ToString();
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
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        MainManager.Instance.scores.Add(new PlayerScore() { name = MainManager.Instance.name, score = m_Points }); ;
        SceneManager.LoadScene(2);
    }
}
