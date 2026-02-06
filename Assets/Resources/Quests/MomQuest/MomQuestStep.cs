using HeneGames.DialogueSystem;
using System;
using UnityEngine;

public class MomQuestStep : QuestStep
{
    private int menCollected = 0;
    private const int menRequired = 1;

    private void OnEnable()
    {
        BulletTargetHeart.OnManInLove += ManCollected;
    }

    private void OnDisable()
    {
        BulletTargetHeart.OnManInLove -= ManCollected;
    }

    public static event Action onMenCollectedChanged;
    private void ManCollected()
    {         
        if (menCollected < menRequired)
        {
            menCollected++;
        }
        if (menCollected >= menRequired)
        {
            onMenCollectedChanged?.Invoke();
            FinishQuestStep();
        }
    }

}
