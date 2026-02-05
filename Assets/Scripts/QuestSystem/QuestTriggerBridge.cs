using UnityEngine;

public class QuestTriggerBridge : MonoBehaviour
{
    [SerializeField] private QuestInfoSO questInfo;
        public void Interact()
    {
        if (QuestManager.instance == null)
        {
            Debug.LogWarning("QuestManager instance is null");
            return;
        }

        string questId = questInfo.id;
        QuestState currentState = QuestManager.instance.GetQuestState(questId);

        switch (currentState)
        {
            case QuestState.CAN_START:
                GameEventsManager.instance.questEvents.StartQuest(questId);
                break;

            case QuestState.IN_PROGRESS:
                Debug.Log("NPC: You're still working on it!");
                break;

            case QuestState.CAN_COMPLETE:
                GameEventsManager.instance.questEvents.FinishQuest(questId);
                break;

            case QuestState.COMPLETED:
                Debug.Log("NPC: Thanks for the help!");
                break;
        }
    }
}

