using HeneGames.DialogueSystem;
using System;
using UnityEngine;

public class Love2QuestStep : QuestStep
{
    private int menCollected;
    private const int menRequired = 5;

    private void OnEnable()
    {
        BulletTargetHeart.OnManInLove += ManCollected; // += means subscribe
    }

    private void OnDisable()
    {
        BulletTargetHeart.OnManInLove -= ManCollected; // -= means unsubscribe
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
