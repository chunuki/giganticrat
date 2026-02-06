using HeneGames.DialogueSystem;
using System;
using UnityEngine;

public class MomQuestStep : QuestStep
{
    private void OnEnable()
    {
        Health.onAkioDied += MenCollected;
    }

    private void OnDisable()
    {
        Health.onAkioDied -= MenCollected;
    }

    public static event Action onMenCollectedChanged;
    private void MenCollected()
    {
        onMenCollectedChanged?.Invoke();
        FinishQuestStep();
    }


}
