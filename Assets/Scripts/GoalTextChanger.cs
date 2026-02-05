using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class GoalTextChanger : MonoBehaviour
{
    public TMP_Text goalsText;
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += AddToGoals;
        GameEventsManager.instance.questEvents.onFinishQuest += RemoveFromGoals;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= AddToGoals;
        GameEventsManager.instance.questEvents.onFinishQuest -= RemoveFromGoals;
    }

    private void AddToGoals(string id)
    {
        Quest quest = QuestManager.instance.GetQuestById(id);
        if (quest != null)
        {
            goalsText.text += $"\n- {quest.info.displayName}";
        }
    }

    private void RemoveFromGoals(string id)
    {
        Quest quest = QuestManager.instance.GetQuestById(id);
        Debug.Log("Removing goal: " + quest.info.displayName);
        if (quest != null)
        {
            goalsText.text = goalsText.text.Replace($"\n- {quest.info.displayName}", "");
        }
    }
}
