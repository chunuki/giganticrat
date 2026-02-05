using HeneGames.DialogueSystem;
using System;
using UnityEngine;

public class Love1QuestStep : QuestStep
{
    private int menCollected = 0;
    private const int menRequired = 1;

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
