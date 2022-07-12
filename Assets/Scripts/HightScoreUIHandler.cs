using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static MainManager;

public class HightScoreUIHandler : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public RectTransform AllScoreContent;
    public TextMeshProUGUI ScoreTextPrelab;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
        SetAllScore();
    }

    private void SetText()
    {
        PlayerScore lastScore = MainManager.Instance.scores.Last();
        ScoreText.text = "Your score (" + lastScore.name + ") : " + lastScore.score.ToString();
        //Debug.Log(text);
        //ScoreText.text = text;
    }

    private void SetAllScore()
    {
        int heigth = 0;
            foreach (PlayerScore score in MainManager.Instance.scores.OrderBy(x => x.score))
            {
                TextMeshProUGUI textScore = Instantiate(ScoreTextPrelab);
                textScore.text = score.name + " : " + score.score;
                textScore.transform.position = new Vector3(20, heigth);
                textScore.transform.SetParent(AllScoreContent, false);
                heigth += 25;
            }
        AllScoreContent.sizeDelta = new Vector2(AllScoreContent.sizeDelta.x, heigth*2);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
