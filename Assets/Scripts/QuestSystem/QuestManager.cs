using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
        Quest quest = GetQuestById("Love1Quest");
        Debug.Log(quest.info.displayName);
    }
    private Dictionary<string, Quest> CreateQuestMap()
    {
        // loads all scriptable objects under Quests folder in Resources
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        // create quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning($"Duplicate quest id found: {questInfo.id}. Quest ids must be unique.");
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError($"Quest with id {id} not found.");
        }
        return quest;
    }
}
