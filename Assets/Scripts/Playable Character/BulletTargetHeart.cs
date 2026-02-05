using UnityEngine;
using System;
using System.Collections;

public class BulletTargetHeart : MonoBehaviour
{
    public HotGuy hotGuy;
    public static event Action OnManInLove; // growtopia superbroadcast type shi

    public void GetShot()
    {
        if (!hotGuy.isDead && !hotGuy.isInLove)
        {
            OnManInLove?.Invoke();
            StartCoroutine(ShotAnimation());
            Debug.Log("He is in Love!");
        }
    }

    private IEnumerator ShotAnimation()
    {
        hotGuy.animator.SetTrigger("IsInLove");
        yield return null;
        yield return new WaitUntil(() => hotGuy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !hotGuy.animator.IsInTransition(0));
        hotGuy.isInLove = true;

    }
}