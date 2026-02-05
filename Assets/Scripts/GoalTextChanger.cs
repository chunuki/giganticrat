using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;

public class GoalTextChanger : MonoBehaviour
{
    public TMP_Text goalstext;
    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += AddToGoals;
        GameEventsManager.instance.questEvents.onFinishQuest += RemoveFromGoals;
    }

    private void OnDisable()
    {
    }

    private void AddToGoals(string id)
    {
        Quest quest = QuestManager.instance.GetQuestById(id);
        if (quest != null)
        {
            TextMeshProUGUI goalsText = GetComponent<TextMeshProUGUI>();
            goalsText.text += $"\n- {quest.info.displayName}";
        }
    }

    private void RemoveFromGoals(string id)
    {
        Quest quest = QuestManager.instance.GetQuestById(id);
        if (quest != null)
        {
            TextMeshProUGUI goalsText = GetComponent<TextMeshProUGUI>();
            goalsText.text = goalsText.text.Replace($"\n- {quest.info.displayName}", "");
        }
    }
}
