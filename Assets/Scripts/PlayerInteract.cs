using UnityEngine;
using StarterAssets;
using System;
public class PlayerInteract : MonoBehaviour
{

    private int menCollected = 0;

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
        menCollected++;
    }

    public int GetMenCollected()
    {
        return menCollected;
    }
}
