using HeneGames.DialogueSystem;
using UnityEngine;

public class ElizabethWarrenDM : MonoBehaviour
{
    private DialogueManager dialogueManager;
    [SerializeField] private DialogueData dialogueData2;
    [SerializeField] private DialogueData dialogueData3;
    [Header("Config")]
    [SerializeField] private QuestInfoSO questInfo1;
    [SerializeField] private QuestInfoSO questInfo2;

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
    }

    private void SwitchToDialogue2()
    {
        dialogueManager.currentDialogueData = dialogueData2;
    }
    private void SwitchToDialogue3()
    {
        dialogueManager.currentDialogueData = dialogueData3;
    }

    private void OnDisable()
    {
        Love1QuestStep.onMenCollectedChanged -= SwitchToDialogue2;
        Love2QuestStep.onMenCollectedChanged -= SwitchToDialogue3;
    }
}