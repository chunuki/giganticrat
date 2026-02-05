using UnityEngine;

public class Love1QuestStep : QuestStep
{
    private int menCollected = 0;
    private const int menRequired = 1;

    private void OnEnable()
    {
        Debug.Log("love quest complete");
        BulletTargetHeart.OnManInLove += ManCollected; // += means subscribe
    }
    private void OnDisable()
    {
        BulletTargetHeart.OnManInLove -= ManCollected; // -= means unsubscribe
    }
    private void ManCollected()
    {         
        if (menCollected < menRequired)
        {
            menCollected++;
        }
        if (menCollected >= menRequired)
        {
            FinishQuestStep();
        }
    }

}
