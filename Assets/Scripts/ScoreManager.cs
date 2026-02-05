using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
