using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private int score = 0;
    private void OnEnable()
    {
        GameEventsManager.instance.scoreEvents.onScoreGained += Score;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.scoreEvents.onScoreGained -= Score;
    }
    private void Score(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
