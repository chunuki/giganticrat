using UnityEngine;
using System;

public class BulletTargetHeart : MonoBehaviour
{
    public HotGuy hotGuy;
    public static event Action OnManInLove; // growtopia superbroadcast type shi

    public void GetShot()
    {
        if (!hotGuy.isDead && !hotGuy.isInLove)
        {
            hotGuy.isInLove = true;
            OnManInLove?.Invoke();
        }
    }
}
