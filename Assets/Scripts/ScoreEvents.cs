using UnityEngine;
using System;

public class ScoreEvents
{
    public event Action<int> onScoreGained;
    public void ScoreGained(int score)
    {
        onScoreGained?.Invoke(score);
    }
}
