using HeneGames.DialogueSystem;
using System;
using UnityEngine;

public class MenQuestStep : QuestStep
{
    private int menCollected = 0;
    private const int menRequired = 5;

    private void OnEnable()
    {
        BulletTargetHeart.OnManInLove += ManCollected;
    }

    private void OnDisable()
    {
        BulletTargetHeart.OnManInLove -= ManCollected;
    }

    private void ManCollected()
    {         
        if (menCollected < menRequired)
        {
            menCollected++;
        }
        if (menCollected >= menRequired)
        {
            MenCollected();
        }
    }

    public static event Action onMenCollectedChanged;
    private void MenCollected()
    {
        onMenCollectedChanged?.Invoke();
        FinishQuestStep();
        Debug.Log("quest3 step complete");
    }

}
