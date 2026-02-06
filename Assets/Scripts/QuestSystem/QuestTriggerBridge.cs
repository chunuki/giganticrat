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
                Debug.Log("1 // havent start yet " + questId);
                GameEventsManager.instance.questEvents.StartQuest(questId);
                break;

            case QuestState.IN_PROGRESS:
                Debug.Log("2 // started in progress " + questId);
                break;

            case QuestState.CAN_COMPLETE:
                Debug.Log("3 // all good can complete " + questId);
                GameEventsManager.instance.questEvents.FinishQuest(questId);
                break;

            case QuestState.COMPLETED:
                Debug.Log("4 // completed " + questId);
                break;
        }
    }
}

