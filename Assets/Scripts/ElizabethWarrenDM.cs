using HeneGames.DialogueSystem;
using UnityEngine;

public class ElizabethWarrenDM : MonoBehaviour
{
    private DialogueManager dialogueManager;
    [Header("Dialogues")]
    [SerializeField] private DialogueData dialogueData2;
    [SerializeField] private DialogueData dialogueData3;
    [SerializeField] private DialogueData dialogueData4;
    [SerializeField] private DialogueData dialogueData5;
    [SerializeField] private DialogueData dialogueData6;

    [Header("Quests")]
    [SerializeField] private QuestInfoSO questInfo1;
    [SerializeField] private QuestInfoSO questInfo2;
    [SerializeField] private QuestInfoSO questInfo3;
    [SerializeField] private QuestInfoSO questInfoMom;
    [SerializeField] private QuestInfoSO questInfoMen;

    private string questId;

    private void Awake()
    {
        if (dialogueManager == null)
        {
            dialogueManager = GetComponent<DialogueManager>();
        }
    }

    private void OnEnable()
    {
        Love1QuestStep.onMenCollectedChanged += SwitchToDialogue2;
        Love2QuestStep.onMenCollectedChanged += SwitchToDialogue3;
        Love3QuestStep.onMenCollectedChanged += SwitchToDialogue4;
        MenQuestStep.onMenCollectedChanged += SwitchToDialogue5;
        MomQuestStep.onMenCollectedChanged += SwitchToDialogue6;
    }

    private void SwitchToDialogue2()
    {
        dialogueManager.currentDialogueData = dialogueData2;
    }
    private void SwitchToDialogue3()
    {
        dialogueManager.currentDialogueData = dialogueData3;
    }
    private void SwitchToDialogue4()
    {
        dialogueManager.currentDialogueData = dialogueData4;
    }
    private void SwitchToDialogue5()
    {
        dialogueManager.currentDialogueData = dialogueData5;
    }
    private void SwitchToDialogue6()
    {
        dialogueManager.currentDialogueData = dialogueData6;
    }

    private void OnDisable()
    {
        Love1QuestStep.onMenCollectedChanged -= SwitchToDialogue2;
        Love2QuestStep.onMenCollectedChanged -= SwitchToDialogue3;
        Love3QuestStep.onMenCollectedChanged -= SwitchToDialogue4;
        MenQuestStep.onMenCollectedChanged -= SwitchToDialogue5;
        MomQuestStep.onMenCollectedChanged -= SwitchToDialogue6;
    }
}