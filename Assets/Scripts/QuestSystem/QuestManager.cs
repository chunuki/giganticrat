using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Quest Manager in the scene.");
        }
        instance = this;
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);

            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    public QuestState GetQuestState(string id)
    {
        if (questMap.ContainsKey(id))
        {
            return questMap[id].state;
        }
        return QuestState.REQUIREMENTS_NOT_MET;
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrequisites)
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.COMPLETED)
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }

    private void StartQuest(string id)
    {
        Debug.Log("quest started");
        Quest quest = GetQuestById(id);
        if (quest.state == QuestState.CAN_START)
        {
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        }
            
    }

    private void AdvanceQuest(string id)
    {
        Debug.Log("quest advanced");
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else
        {             
            ChangeQuestState(quest.info.id, QuestState.CAN_COMPLETE);
        }
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.COMPLETED);
    }

    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.instance.scoreEvents.ScoreGained(quest.info.scoreReward);
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

    public Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("quest id not found");
        }
        return quest;
    }
}
