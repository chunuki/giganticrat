using HeneGames.DialogueSystem;
using UnityEngine;

public class ElizabethWarrenDM : MonoBehaviour
{
    private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueData2;
    [Header("Config")]
    [SerializeField] private QuestInfoSO questInfo;

    private string questId;

    private void Awake()
    {
        if (dialogueManager == null)
        {
            dialogueManager = GetComponent<DialogueManager>();
        }
        questId = questInfo.id;
    }

    private void OnEnable()
    {
        Love1QuestStep.onMenCollectedChanged += SwitchToDialogue2;
    }

    private void SwitchToDialogue2()
    {
        dialogueManager.currentDialogueData = dialogueData2;
    }

    private void OnDisable()
    {
        Love1QuestStep.onMenCollectedChanged -= SwitchToDialogue2;
    }
}