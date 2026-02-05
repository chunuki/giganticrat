using UnityEngine;

public class QuestTriggerBridge : MonoBehaviour
{
    [SerializeField] private QuestInfoSO questInfo;

    public void TriggerStartQuest()
    {
        GameEventsManager.instance.questEvents.StartQuest(questInfo.id);
    }
}

