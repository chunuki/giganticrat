using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public QuestEvents questEvents = new QuestEvents();
    public ScoreEvents scoreEvents = new ScoreEvents();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        questEvents = new QuestEvents();
        scoreEvents = new ScoreEvents();
    }
}
